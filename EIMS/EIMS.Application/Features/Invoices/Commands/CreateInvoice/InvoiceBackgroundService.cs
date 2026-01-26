using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class InvoiceBackgroundService : IInvoiceBackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InvoiceBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        // Hàm này sẽ chạy ngầm, không bắt người dùng chờ
        public void ProcessInvoiceCreation(int invoiceId, int userId, int? requestId)
        {
            // QUAN TRỌNG: Phải dùng Task.Run để tách luồng
            Task.Run(async () =>
            {
                // QUAN TRỌNG: Tạo Scope mới vì Scope cũ của Request đã bị hủy
                using (var scope = _scopeFactory.CreateScope())
                {
                    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                    var invoiceXMLService = scope.ServiceProvider.GetRequiredService<IInvoiceXMLService>();
                    var pdfService = scope.ServiceProvider.GetRequiredService<IPdfService>();
                    var fileStorageService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
                    var notiService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                    var invoiceRealtimeService = scope.ServiceProvider.GetRequiredService<IInvoiceRealtimeService>();
                    var dashboardRealtimeService = scope.ServiceProvider.GetRequiredService<IDashboardRealtimeService>();

                    string? xmlPath = null;
                    try
                    {
                        // 1. Lấy lại Invoice đầy đủ thông tin
                        var fullInvoice = await uow.InvoicesRepository
                            .GetByIdAsync(invoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus,Template.Serial.InvoiceType,InvoiceStatus,Company");

                        if (fullInvoice == null) return;

                        // 2. Tạo Symbol
                        var symbol = await uow.InvoicesRepository.GetInvoiceSymbolAsync(fullInvoice.InvoiceID);
                        fullInvoice.InvoiceSymbol = symbol.FullSymbol;
                        await uow.SaveChanges();

                        // 3. Tạo XML & Upload
                        string newXmlUrl = await invoiceXMLService.GenerateAndUploadXmlAsync(fullInvoice);
                        fullInvoice.XMLPath = newXmlUrl;
                        await uow.InvoicesRepository.UpdateAsync(fullInvoice);
                        await uow.SaveChanges();

                        // 4. Tạo PDF & Upload
                        try
                        {
                            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                            byte[] pdfBytes = await pdfService.ConvertXmlToPdfAsync(fullInvoice.InvoiceID, rootPath);
                            using (var pdfStream = new MemoryStream(pdfBytes))
                            {
                                string fileName = $"Invoice_{fullInvoice.InvoiceNumber}_{Guid.NewGuid()}.pdf";
                                var uploadResult = await fileStorageService.UploadFileAsync(pdfStream, fileName, "invoices");

                                if (uploadResult.IsSuccess)
                                {
                                    fullInvoice.FilePath = uploadResult.Value.Url;
                                    await uow.InvoicesRepository.UpdateAsync(fullInvoice);
                                    await uow.SaveChanges();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"PDF Gen Error: {ex.Message}");
                            // Không throw lỗi ở đây để code vẫn chạy tiếp phần thông báo
                        }

                        // 5. Cập nhật Invoice Request (nếu có)
                        if (requestId != null && requestId != 0)
                        {
                            var invoiceRequest = await uow.InvoiceRequestRepository.GetByIdAsync(requestId.Value);
                            if (invoiceRequest != null)
                            {
                                invoiceRequest.RequestStatusID = (int)EInvoiceRequestStatus.Approved;
                                invoiceRequest.CreatedInvoiceID = fullInvoice.InvoiceID;
                                await uow.InvoiceRequestRepository.UpdateAsync(invoiceRequest);

                                await notiService.SendToUserAsync(invoiceRequest.SaleID ?? 0,
                                    $"Yêu cầu cấp hóa đơn ID {invoiceRequest.RequestID} đã được chấp thuận.", typeId: 2);
                            }
                        }

                        // 6. Gửi thông báo cho HOD
                        await notiService.SendToRoleAsync("HOD", $"Có hóa đơn mới {symbol.FullSymbol}_{fullInvoice.InvoiceNumber} đã được khởi tạo.", typeId: 2);

                        // 7. Realtime Updates (SignalR)
                        await invoiceRealtimeService.NotifyInvoiceChangedAsync(new InvoiceRealtimeEvent
                        {
                            InvoiceId = fullInvoice.InvoiceID,
                            ChangeType = "Created", // Hoặc "FileGenerated"
                            CompanyId = fullInvoice.CompanyId,
                            CustomerId = fullInvoice.CustomerID,
                            StatusId = fullInvoice.InvoiceStatusID,
                            Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
                        }, CancellationToken.None);

                        await dashboardRealtimeService.NotifyDashboardChangedAsync(new DashboardRealtimeEvent
                        {
                            Scope = "Invoices",
                            ChangeType = "Created",
                            EntityId = fullInvoice.InvoiceID,
                            Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
                        }, CancellationToken.None);

                        await uow.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Background Job Error: {ex}");
                    }
                }
            });
        }
    }
}
