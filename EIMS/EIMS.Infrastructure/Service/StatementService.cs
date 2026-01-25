using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Application.DTOs.XMLModels.PaymentStatements;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class StatementService : IStatementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<StatementProductDto> CalculateStatementProductSummary(IEnumerable<Invoice> invoices)
        {
            if (invoices == null || !invoices.Any()) return new List<StatementProductDto>();

            // 1. Flatten: Gom tất cả item từ các hóa đơn
            var allItems = invoices.SelectMany(i => i.InvoiceItems).ToList();

            // 2. Group & Calculate (Logic y hệt bên PDF)
            var summary = allItems
                .GroupBy(item => new
                {
                    ProductID = item.ProductID,
                    ProductName = item.Product?.Name ?? "Khác",
                    Unit = item.Product?.Unit ?? ""
                })
                .Select(g =>
                {
                    double totalQty = g.Sum(x => x.Quantity);
                    decimal totalAmt = g.Sum(x => x.Amount);
                    decimal totalVat = g.Sum(x => x.VATAmount);
                    if (totalQty == 0 && totalAmt != 0) totalQty = 1;

                    decimal avgPrice = totalQty != 0 ? totalAmt / (decimal)totalQty : 0;

                    return new StatementProductDto
                    {
                        ProductId = g.Key.ProductID,
                        ProductName = g.Key.ProductName,
                        Unit = g.Key.Unit,
                        Quantity = totalQty,
                        UnitPrice = avgPrice,
                        TotalAmount = totalAmt + totalVat, // Tổng thanh toán
                        VATAmount = totalVat
                    };
                })
                .OrderBy(x => x.ProductName)
                .ToList();

            return summary;
        }
        public async Task<PaymentStatementDTO> GetPaymentRequestXmlAsync(InvoiceStatement statementEntity)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
            if (company == null) throw new Exception("Company not found");
            if (statementEntity == null) throw new Exception("Statement not found");

            // 1. FIX THỜI GIAN: Dùng PeriodMonth/Year thay vì StatementDate
            var startOfStatementPeriod = new DateTime(statementEntity.PeriodYear, statementEntity.PeriodMonth, 1);
            // Ngày đầu tháng sau (để giới hạn phạm vi)
            var startOfNextMonth = startOfStatementPeriod.AddMonths(1);

            var xmlDto = new PaymentStatementDTO
            {
                ProviderInfo = new ProviderInfoDTO
                {
                    Name = company.CompanyName,
                    Address = company.Address,
                    Phone = company.ContactPhone,
                    LogoUrl = company.LogoUrl
                },
                HeaderInfo = new HeaderInfoDTO
                {
                    AccountantName = statementEntity.Creator?.FullName ?? "Admin",
                    StatementDate = statementEntity.StatementDate.ToString("dd/MM/yyyy"),
                    // Hiển thị kỳ báo cáo đúng theo tháng được chọn
                    StatementMonth = $"{statementEntity.PeriodMonth:D2}/{statementEntity.PeriodYear}",
                    CustomerName = statementEntity.Customer?.CustomerName ?? "Unknown Customer",
                    CustomerCode = $"KHACVT_{statementEntity.Customer?.CustomerID}",
                    ContactEmail = "mannvse181589@fpt.edu.vn"
                },
                Items = new List<StatementItemDTO>()
            };

            var allDetails = statementEntity.StatementDetails.ToList();

            // --- PHẦN A: NỢ CŨ (Trước tháng báo cáo) ---
            // Điều kiện: Ngày hóa đơn < Ngày 1 của tháng báo cáo
            if (statementEntity.OpeningBalance > 0)
            {
                var openingItem = new StatementItemDTO
                {
                    // Diễn giải chung
                    Description = "Số dư nợ đầu kỳ (Opening Balance)",

                    // Thời gian: Là thời điểm trước ngày bắt đầu kỳ báo cáo
                    PeriodFrom = startOfStatementPeriod.AddDays(-1).ToString("dd/MM/yyyy"),
                    PeriodTo = startOfStatementPeriod.AddDays(-1).ToString("dd/MM/yyyy"),

                    IndicatorOld = "1",
                    Quantity = 1,

                    // Lấy trực tiếp từ field OpeningBalance đã lưu
                    UnitPrice = statementEntity.OpeningBalance,
                    Amount = statementEntity.OpeningBalance,

                    VATRate = 0,
                    VATAmount = 0,
                    TotalCurrent = 0,

                    // Map vào cột nợ cũ
                    PreviousDebt = statementEntity.OpeningBalance,
                    TotalPayable = statementEntity.OpeningBalance
                };

                xmlDto.Items.Add(openingItem);
            }

            // --- PHẦN B: PHÁT SINH TRONG KỲ (Hóa đơn thuộc tháng báo cáo) ---
            // Điều kiện: >= Ngày 1 tháng báo cáo VÀ < Ngày 1 tháng sau
            var currentInvoices = allDetails
                .Where(d => (d.Invoice.IssuedDate ?? DateTime.MinValue) >= startOfStatementPeriod
                         && (d.Invoice.IssuedDate ?? DateTime.MinValue) < startOfNextMonth)
                .Select(d => d.Invoice)
                .Where(i => i != null)
                .ToList();

            if (currentInvoices.Any())
            {
                var allProductItems = currentInvoices.SelectMany(inv => inv.InvoiceItems).ToList();

                var groupedProducts = allProductItems
                    .GroupBy(item => new
                    {
                        item.ProductID,
                        ProductName = item.Product?.Name ?? "Sản phẩm khác",
                        Unit = item.Product?.Unit ?? ""
                    })
                    .Select(g =>
                    {
                        double totalQuantity = g.Sum(x => x.Quantity);
                        decimal totalAmountBeforeTax = g.Sum(x => x.Amount);
                        decimal totalVATAmount = g.Sum(x => x.VATAmount);
                        decimal totalAmountAfterTax = totalAmountBeforeTax + totalVATAmount;

                        // 2. FIX DIVIDE BY ZERO
                        decimal averageUnitPrice = totalQuantity != 0 ? totalAmountBeforeTax / (decimal)totalQuantity : 0;
                        // Tính VAT Rate tương đối (hoặc lấy từ item đầu tiên nếu chắc chắn giống nhau)
                        double vatRate = (totalAmountBeforeTax != 0)
                                        ? (double)Math.Round((totalVATAmount / totalAmountBeforeTax) * 100, 0) // Làm tròn VAT
                                        : 0;

                        return new StatementItemDTO
                        {
                            Description = g.Key.ProductName,
                            PeriodFrom = startOfStatementPeriod.ToString("dd/MM/yyyy"),
                            PeriodTo = startOfStatementPeriod.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy"),
                            IndicatorOld = "0", // Đánh dấu là mới

                            Quantity = (decimal)totalQuantity,
                            UnitPrice = averageUnitPrice,
                            Amount = totalAmountBeforeTax,

                            VATRate = vatRate,
                            VATAmount = totalVATAmount,

                            // Đây là tổng tiền phát sinh MỚI
                            TotalCurrent = totalAmountAfterTax,
                            PreviousDebt = 0,

                            // Lưu ý: Ở dòng chi tiết sản phẩm, ta vẫn hiện TotalPayable là full giá trị
                            // Việc trừ tiền đã trả sẽ nằm ở phần Summary tổng
                            TotalPayable = totalAmountAfterTax
                        };
                    })
                    .OrderBy(x => x.Description)
                    .ToList();

                xmlDto.Items.AddRange(groupedProducts);
            }
            if (statementEntity.PaidAmount > 0)
            {
                var paymentItem = new StatementItemDTO
                {
                    // 1. Diễn giải
                    Description = $"Số tiền trả tháng này ({statementEntity.PeriodMonth:00}/{statementEntity.PeriodYear})",
                    TotalPayable = -statementEntity.PaidAmount
                };

                xmlDto.Items.Add(paymentItem);
            }
            // --- PHẦN C: TỔNG HỢP (QUAN TRỌNG) ---

            // Tính tổng phát sinh (Full giá trị hóa đơn mới)
            decimal totalCurrentPeriodFull = xmlDto.Items.Where(x => x.PreviousDebt == 0 && x.Amount > 0).Sum(x => x.TotalCurrent);
            // Tính tổng nợ cũ
            decimal totalPreviousDebt = xmlDto.Items.Sum(x => x.PreviousDebt);

            // Lấy số tiền đã thanh toán trong kỳ (Từ Entity đã lưu ở bước Handle)
            decimal paidAmount = statementEntity.PaidAmount;

            xmlDto.Summary = new StatementSummaryDTO
            {
                TotalAmount = xmlDto.Items.Where(x => x.Amount > 0).Sum(x => x.Amount), // Tổng tiền hàng
                TotalVAT = xmlDto.Items.Sum(x => x.VATAmount), // Tổng tiền thuế

                TotalCurrentPeriod = totalCurrentPeriodFull,   // Tổng cộng phát sinh
                TotalPreviousDebt = totalPreviousDebt,         // Tổng nợ cũ

                // Cần thêm trường này vào DTO nếu chưa có để hiển thị trên PDF: "Đã thanh toán"
                // PaidAmount = paidAmount, 

                // 3. FIX CÔNG THỨC FINAL:
                // Phải thu cuối kỳ = (Nợ cũ + Phát sinh mới) - Đã trả
                GrandTotal = (totalPreviousDebt + totalCurrentPeriodFull) - paidAmount
            };

            return xmlDto;
        }
    }
}
