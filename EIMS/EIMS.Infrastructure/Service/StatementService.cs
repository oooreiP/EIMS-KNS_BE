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
                .GroupBy(item => new {
                    ProductID = item.ProductID,
                    ProductName = item.Product?.Name  ?? "Khác",
                    Unit = item.Product?.Unit  ?? ""
                })
                .Select(g =>
                {
                    double totalQty = g.Sum(x => x.Quantity );
                    decimal totalAmt = g.Sum(x => x.Amount);
                    decimal totalVat = g.Sum(x => x.VATAmount);
                    if (totalQty == 0 && totalAmt != 0) totalQty = 1;

                    decimal avgPrice = totalQty != 0 ? totalAmt /(decimal) totalQty : 0;

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
            var xmlDto = new PaymentStatementDTO
            {
                ProviderInfo = new ProviderInfoDTO
                {
                    Name = company.CompanyName,
                    Address = company.Address,
                    Phone = company.ContactPhone
                },

                // B. Thông tin Header
                HeaderInfo = new HeaderInfoDTO
                {
                    AccountantName = statementEntity.Creator.FullName,
                    StatementDate = statementEntity.StatementDate.ToString("dd/MM/yyyy"),
                    StatementMonth = statementEntity.StatementDate.ToString("MM/yyyy"),
                    CustomerName = statementEntity.Customer?.CustomerName ?? "Unknown Customer",
                    CustomerCode = $"KHACVT_{statementEntity.Customer?.CustomerID}",
                    ContactEmail = "support@einvoice.vn"
                },
                Items = new List<StatementItemDTO>()
            };
            var startOfStatementPeriod = new DateTime(statementEntity.StatementDate.Year, statementEntity.StatementDate.Month, 1);
            var allDetails = statementEntity.StatementDetails.ToList();
            var oldDetails = allDetails
                .Where(d => (d.Invoice.IssuedDate ?? DateTime.MinValue) < startOfStatementPeriod)
                .ToList();

            if (oldDetails.Any())
            {
                var oldItemsSummary = oldDetails
                    .GroupBy(d => new {
                        Month = d.Invoice.IssuedDate?.Month ?? 0,
                        Year = d.Invoice.IssuedDate?.Year ?? 0
                    })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => new StatementItemDTO
                    {
                        // Diễn giải gom nhóm: "Dư nợ quá hạn tháng 09/2025"
                        Description = $"Dư nợ quá hạn tháng {g.Key.Month.ToString("00")}/{g.Key.Year}",

                        // Thời gian
                        PeriodFrom = $"{g.Key.Month.ToString("00")}/{g.Key.Year}",
                        PeriodTo = $"{g.Key.Month.ToString("00")}/{g.Key.Year}",

                        IndicatorOld = "0",
                        IndicatorNew = "0",
                        Quantity = 1,

                        // --- LOGIC QUAN TRỌNG CHO NỢ CŨ ---
                        // Chỉ lấy OutstandingAmount (Số tiền còn lại phải trả)
                        // Không hiển thị VAT hay Doanh thu gốc vì đã kê khai ở tháng trước rồi

                        UnitPrice = g.Sum(x => x.OutstandingAmount),
                        Amount = g.Sum(x => x.OutstandingAmount),

                        VATRate = 0, // Để trống
                        VATAmount = 0, // Coi như bằng 0 để không cộng dồn thuế sai kỳ

                        TotalCurrent = 0, // Không phải phát sinh kỳ này
                        PreviousDebt = g.Sum(x => x.OutstandingAmount), // Đưa vào cột Nợ cũ (nếu template có cột này)

                        TotalPayable = g.Sum(x => x.OutstandingAmount) // Tổng phải trả = Tổng số dư nợ
                    });

                xmlDto.Items.AddRange(oldItemsSummary);
            }

            // --- PHẦN B: XỬ LÝ PHÁT SINH TRONG KỲ (Tháng hiện tại) ---
            // Điều kiện: Ngày hóa đơn >= Ngày đầu tháng này
            var currentInvoices = allDetails
         .Where(d => (d.Invoice.IssuedDate ?? DateTime.MinValue) >= startOfStatementPeriod)
         .Select(d => d.Invoice)
         .Where(i => i != null) // Safety check
         .ToList();

            if (currentInvoices.Any())
            {
                // 2. Gom tất cả InvoiceItems từ các hóa đơn này lại thành 1 danh sách lớn
                var allProductItems = currentInvoices
                    .SelectMany(inv => inv.InvoiceItems) // Flatten: Trải phẳng list trong list
                    .ToList();

                // 3. Group theo ProductID (hoặc ProductName nếu muốn gộp theo tên)
                var groupedProducts = allProductItems
                    .GroupBy(item => new {
                        item.ProductID,
                        ProductName = item.Product?.Name ?? "Sản phẩm khác",
                        Unit = item.Product?.Unit ?? ""
                    })
                    .Select(g =>
                    {
                        // Tính toán tổng số lượng và thành tiền
                        double totalQuantity = g.Sum(x => x.Quantity);
                        decimal totalAmountBeforeTax = g.Sum(x => x.Amount);
                        decimal totalVATAmount = g.Sum(x => x.VATAmount);
                        decimal totalAmountAfterTax = totalAmountBeforeTax + totalVATAmount;

                        // Tính đơn giá trung bình (để hiển thị cho hợp lý nếu các hóa đơn có giá khác nhau)
                        // Nếu Quantity = 0 thì tránh chia cho 0
                        decimal averageUnitPrice = totalQuantity != 0
                            ? totalAmountBeforeTax / (decimal)totalQuantity
                            : 0;

                        return new StatementItemDTO
                        {
                            // Hiển thị tên sản phẩm
                            Description = g.Key.ProductName,

                            // Thời gian: Lấy tháng hiện tại
                            PeriodFrom = startOfStatementPeriod.ToString("dd/MM/yyyy"),
                            PeriodTo = startOfStatementPeriod.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy"),

                            IndicatorOld = "0",
                            IndicatorNew = "0",

                            // Các con số tổng hợp
                            Quantity = (decimal)totalQuantity,
                            UnitPrice = averageUnitPrice, // Đơn giá trung bình
                            Amount = totalAmountBeforeTax, // Tổng tiền trước thuế

                            // Thuế
                            VATRate = 0, // Khó để hiển thị 1 mức thuế nếu gộp nhiều dòng, hoặc bạn có thể lấy g.First().VATRate
                            VATAmount = totalVATAmount,

                            // Tổng tiền thanh toán
                            TotalCurrent = totalAmountAfterTax,
                            PreviousDebt = 0,
                            TotalPayable = totalAmountAfterTax
                        };
                    })
                    .OrderBy(x => x.Description) // Sắp xếp theo tên cho đẹp
                    .ToList();

                xmlDto.Items.AddRange(groupedProducts);
            }

            // --- PHẦN C: TỔNG HỢP CUỐI CÙNG (Cập nhật lại công thức tính) ---
            xmlDto.Summary = new StatementSummaryDTO
            {
                // Tổng tiền hàng (trước thuế) của các item trong danh sách
                TotalAmount = xmlDto.Items.Sum(x => x.Amount),

                // Tổng tiền thuế
                TotalVAT = xmlDto.Items.Sum(x => x.VATAmount),

                // Tổng phát sinh kỳ này (chỉ lấy những dòng không phải nợ cũ)
                TotalCurrentPeriod = xmlDto.Items.Where(x => x.PreviousDebt == 0).Sum(x => x.TotalCurrent),

                // Tổng nợ cũ
                TotalPreviousDebt = xmlDto.Items.Sum(x => x.PreviousDebt),

                // Tổng cộng phải thanh toán = Phát sinh kỳ này + Nợ cũ
                GrandTotal = xmlDto.Items.Sum(x => x.TotalPayable)
            };

            return xmlDto;
        }
    }
}
