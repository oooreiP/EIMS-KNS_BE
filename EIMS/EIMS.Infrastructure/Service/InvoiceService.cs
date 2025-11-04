using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Domain.Entities;

namespace EIMS.Infrastructure.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Invoice> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("Invoice must contain at least one item.");

            Customer? customer = null;
            if (request.CustomerID == null || request.CustomerID == 0)
            {
                customer = new Customer
                {
                    CustomerName = request.CompanyName ?? request.Name ?? "Khách hàng chưa đặt tên",
                    TaxCode = request.TaxCode ?? "",
                    Address = request.Address ?? "Chưa cập nhật",
                    ContactEmail = "", // tránh null validation
                    ContactPerson = request.Name,
                    ContactPhone = ""
                };

                customer = await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);
            }
            decimal subtotal = 0m;
            decimal vatAmount = 0m;

            foreach (var item in request.Items)
            {
                subtotal += item.Amount;
                vatAmount += item.VATAmount;
            }

            // Nếu FE không gửi hoặc gửi sai => tự động cập nhật
            if (request.Amount <= 0)
                request.Amount = subtotal;

            if (request.TaxAmount <= 0)
                request.TaxAmount = vatAmount;

            if (request.TotalAmount <= 0)
                request.TotalAmount = request.Amount + request.TaxAmount;

            // Tự tính thuế suất trung bình (nếu có VAT)
            decimal vatRate = 0;
            if (request.Amount > 0 && request.TaxAmount > 0)
                vatRate = Math.Round((request.TaxAmount / request.Amount) * 100, 2);

            var invoice = new Invoice
            {
                TemplateID = request.TemplateID ?? 1,
                CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                CreatedAt = DateTime.Now,
                SubtotalAmount = request.Amount,
                VATAmount = request.TaxAmount,
                VATRate = request.TaxAmount / (request.Amount == 0 ? 1 : request.Amount) * 100,
                TotalAmount = request.TotalAmount,
                TotalAmountInWords = ConvertAmountToWords(request.TotalAmount),
                SignDate = DateTime.Now,
                InvoiceStatusID = 1, // default: Draft
                IssuerID = request.SignedBy ?? 1, // default người ký
                InvoiceItems = request.Items?.Select(i => new InvoiceItem
                {
                    ItemName = i.ItemName,
                    Unit = i.Unit,
                    Quantity = i.Quantity,
                    Amount = i.Amount,
                    VATAmount = i.VATAmount,
                    //CategoryID = i.ProductType
                }).ToList() ?? new List<InvoiceItem>()
            };

            return await _unitOfWork.InvoicesRepository.CreateInvoiceAsync(invoice);
        }
        private string ConvertAmountToWords(decimal amount)
        {
            return $"{amount:n0} đồng"; // có thể thay bằng logic tiếng Việt chuẩn
        }
        public async Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _unitOfWork.InvoicesRepository.GetAllAsync(includeProperties: "Items");
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _unitOfWork.InvoicesRepository.GetByIdAsync(id);
        }
    }
}
