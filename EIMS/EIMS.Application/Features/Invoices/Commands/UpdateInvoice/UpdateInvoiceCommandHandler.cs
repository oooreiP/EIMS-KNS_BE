using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.Features.Invoices.Commands.UpdateInvoice;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Fail(new Error("Invoice must have at least one item"));

            string? xmlPath = null;

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1. Fetch Invoice with Company included for validation/mapping
                var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(
                    request.InvoiceId,
                    "InvoiceItems,Customer,Template.Serial,Company"
                );

                if (invoice == null)
                    return Result.Fail(new Error($"Invoice {request.InvoiceId} not found"));

                if (invoice.InvoiceStatusID != 1)
                    return Result.Fail(new Error($"Only Draft invoices can be updated."));
                if (invoice.CompanyId == null)
                {
                    invoice.CompanyId = 1; // The ID we just seeded
                                           // We must manually load the Company property if it was null initially
                    invoice.Company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
                }
                // 2. Update Customer Data (Safety Check logic)
                if (request.CustomerID.HasValue && request.CustomerID.Value > 0)
                {
                    if (invoice.CustomerID != request.CustomerID.Value)
                        invoice.CustomerID = request.CustomerID.Value;
                }
                else
                {
                    bool detailsChanged =
                        (request.CompanyName ?? request.Name) != invoice.Customer.CustomerName ||
                        request.Address != invoice.Customer.Address ||
                        request.TaxCode != invoice.Customer.TaxCode;

                    if (detailsChanged)
                    {
                        int usageCount = await _unitOfWork.InvoicesRepository.CountAsync(i => i.CustomerID == invoice.CustomerID);
                        if (usageCount > 1)
                        {
                            var newCustomer = new Customer
                            {
                                CustomerName = request.CompanyName ?? request.Name ?? "Khách hàng mới",
                                TaxCode = request.TaxCode ?? "",
                                Address = request.Address ?? "",
                                ContactEmail = invoice.Customer.ContactEmail,
                                ContactPerson = request.Name,
                                ContactPhone = invoice.Customer.ContactPhone
                            };
                            newCustomer = await _unitOfWork.CustomerRepository.CreateCustomerAsync(newCustomer);
                            invoice.CustomerID = newCustomer.CustomerID;
                        }
                        else
                        {
                            var customerToUpdate = invoice.Customer;
                            customerToUpdate.CustomerName = request.CompanyName ?? request.Name ?? customerToUpdate.CustomerName;
                            customerToUpdate.TaxCode = request.TaxCode ?? customerToUpdate.TaxCode;
                            customerToUpdate.Address = request.Address ?? customerToUpdate.Address;
                            customerToUpdate.ContactPerson = request.Name ?? customerToUpdate.ContactPerson;
                            await _unitOfWork.CustomerRepository.UpdateAsync(customerToUpdate);
                        }
                    }
                }

                // 3. Update Items
                if (invoice.InvoiceItems != null && invoice.InvoiceItems.Any())
                {
                    invoice.InvoiceItems.Clear();
                }

                foreach (var itemDto in request.Items)
                {
                    invoice.InvoiceItems.Add(new InvoiceItem
                    {
                        InvoiceID = invoice.InvoiceID,
                        ProductID = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        Amount = itemDto.Amount,
                        VATAmount = itemDto.VATAmount,
                        UnitPrice = itemDto.Quantity > 0 ? (itemDto.Amount /  (decimal)itemDto.Quantity) : 0
                    });
                }

                // 4. Update Totals
                decimal subtotal = request.Items.Sum(i => i.Amount);
                decimal vatAmount = request.Items.Sum(i => i.VATAmount);
                invoice.SubtotalAmount = subtotal;
                invoice.VATAmount = vatAmount;
                invoice.TotalAmount = request.TotalAmount > 0 ? request.TotalAmount : (subtotal + vatAmount);
                invoice.VATRate = (invoice.SubtotalAmount > 0 && invoice.VATAmount > 0)
                   ? Math.Round((invoice.VATAmount / invoice.SubtotalAmount) * 100, 2)
                   : 0;
                invoice.TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(invoice.TotalAmount);

                if (request.MinRows.HasValue) invoice.MinRows = request.MinRows.Value;
                if (request.SignedBy.HasValue) invoice.IssuerID = request.SignedBy.Value;

                await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveChanges();

                // 5. Regenerate XML
                // Fetch FULL invoice again with Company included
                var fullInvoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(
                    invoice.InvoiceID,
                    "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus,Template.Serial.InvoiceType,Company" // <--- Added Company here
                );

                var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);
                var serializer = new XmlSerializer(typeof(HDon));
                var fileName = $"Invoice_{fullInvoice.InvoiceNumber}.xml";
                xmlPath = Path.Combine(Path.GetTempPath(), fileName);

                await using (var fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, xmlModel);
                }

                await using var xmlStream = File.OpenRead(xmlPath);
                var uploadResult = await _fileStorageService.UploadFileAsync(xmlStream, fileName, "invoices");

                if (uploadResult.IsSuccess)
                {
                    fullInvoice.XMLPath = uploadResult.Value.Url;
                    await _unitOfWork.InvoicesRepository.UpdateAsync(fullInvoice);
                }

                // 6. Log History
                var history = new InvoiceHistory
                {
                    InvoiceID = invoice.InvoiceID,
                    ActionType = "Updated",
                    Date = DateTime.UtcNow,
                    PerformedBy = request.SignedBy,
                };
                await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);

                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();

                return Result.Ok(invoice.InvoiceID);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result.Fail(new Error($"Failed to update invoice: {ex.Message}").CausedBy(ex));
            }
            finally
            {
                if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
                    File.Delete(xmlPath);
            }
        }
    }
}