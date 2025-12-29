using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.Features.Invoices.Commands.UpdateInvoice;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                    invoice.CompanyId = 1;
                    invoice.Company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
                }
                if (invoice.Payments != null && invoice.Payments.Any())
                {
                    return Result.Fail("Cannot update an invoice that already has recorded payments. Delete the payments first.");
                }
                invoice.Notes = request.Notes;
                invoice.PaymentMethod = request.PaymentMethod;
                // 2. Update Customer Data (Safety Check logic)
                if (request.CustomerID.HasValue && request.CustomerID.Value > 0 && invoice.CustomerID != request.CustomerID.Value)
                {
                    // 1. Link to the new User Account
                    invoice.CustomerID = request.CustomerID.Value;

                    // 2. Fetch that customer to get their default details
                    var newCustomer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerID.Value);
                    if (newCustomer != null)
                    {
                        // 3. Reset the snapshot to the new customer's defaults
                        invoice.InvoiceCustomerName = newCustomer.CustomerName;
                        invoice.InvoiceCustomerAddress = newCustomer.Address;
                        invoice.InvoiceCustomerTaxCode = newCustomer.TaxCode;
                    }
                }

                // Case B: User Manually Edited Name/Address/TaxCode (Overrides everything)
                // We update the SNAPSHOT fields on the invoice, but we keep the CustomerID the same.
                // This ensures the Invoice stays in the User's "My Invoices" list, even if the address is custom.
                
                if (!string.IsNullOrEmpty(request.CustomerName)) 
                    invoice.InvoiceCustomerName = request.CustomerName;
                
                if (!string.IsNullOrEmpty(request.Address)) 
                    invoice.InvoiceCustomerAddress = request.Address;
                
                if (!string.IsNullOrEmpty(request.TaxCode)) 
                    invoice.InvoiceCustomerTaxCode = request.TaxCode;

                // Fallback: If snapshot fields are still null (e.g. old invoices), fill them from the current relation
                if (string.IsNullOrEmpty(invoice.InvoiceCustomerName)) invoice.InvoiceCustomerName = invoice.Customer?.CustomerName;
                if (string.IsNullOrEmpty(invoice.InvoiceCustomerAddress)) invoice.InvoiceCustomerAddress = invoice.Customer?.Address;
                if (string.IsNullOrEmpty(invoice.InvoiceCustomerTaxCode)) invoice.InvoiceCustomerTaxCode = invoice.Customer?.TaxCode;

                // 3. Update Items
                // Fetch Products to get BasePrice and VATRate
                var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
                var products = await _unitOfWork.ProductRepository.GetAllQueryable()
                    .Where(p => productIds.Contains(p.ProductID))
                    .ToListAsync(cancellationToken);
                var productDict = products.ToDictionary(p => p.ProductID);

                if (products.Count != productIds.Count)
                    return Result.Fail(new Error("One or more products not found").WithMetadata("ErrorCode", "Invoice.Update.Failed"));

                // Clear old items
                if (invoice.InvoiceItems != null && invoice.InvoiceItems.Any())
                {
                    invoice.InvoiceItems.Clear();
                }

                var processedItems = new List<InvoiceItem>();

                foreach (var itemReq in request.Items)
                {
                    if (!productDict.ContainsKey(itemReq.ProductId)) continue;

                    var productInfo = productDict[itemReq.ProductId];

                    // Calculate Final Amount
                    decimal finalAmount = (itemReq.Amount ?? 0) > 0
                        ? itemReq.Amount!.Value
                        : productInfo.BasePrice * (decimal)itemReq.Quantity;

                    // Calculate Final VAT
                    decimal finalVatAmount;
                    if ((itemReq.VATAmount ?? 0) > 0)
                    {
                        finalVatAmount = itemReq.VATAmount!.Value;
                    }
                    else
                    {
                        decimal vatRate = productInfo.VATRate ?? 0;
                        finalVatAmount = Math.Round(finalAmount * (vatRate / 100m), 2);
                    }

                    // Add to collection
                    invoice.InvoiceItems.Add(new InvoiceItem
                    {
                        InvoiceID = invoice.InvoiceID,
                        ProductID = itemReq.ProductId,
                        Quantity = itemReq.Quantity,
                        Amount = finalAmount,
                        VATAmount = finalVatAmount,
                        UnitPrice = productInfo.BasePrice
                    });
                }

                // 4. Update Totals
                decimal subtotal = invoice.InvoiceItems.Sum(i => i.Amount);
                decimal vatAmount = invoice.InvoiceItems.Sum(i => i.VATAmount);

                // Use request overrides if provided and > 0, otherwise calculated values
                if ((request.Amount ?? 0) > 0) subtotal = request.Amount!.Value;
                if ((request.TaxAmount ?? 0) > 0) vatAmount = request.TaxAmount!.Value;

                decimal totalAmount = subtotal + vatAmount;
                if ((request.TotalAmount ?? 0) > 0) totalAmount = request.TotalAmount!.Value;

                invoice.SubtotalAmount = subtotal;
                invoice.VATAmount = vatAmount;
                invoice.TotalAmount = totalAmount;
                invoice.VATRate = (subtotal > 0) ? Math.Round((vatAmount / subtotal) * 100, 2) : 0;
                invoice.TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAmount);
                invoice.PaidAmount = 0;
                invoice.RemainingAmount = totalAmount;

                if (request.MinRows.HasValue) invoice.MinRows = request.MinRows.Value;
                if (request.SignedBy.HasValue) invoice.IssuerID = request.SignedBy.Value;

                await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveChanges();
                // 5. Regenerate XML
                // Fetch FULL invoice again with Company included
                var fullInvoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(
                    invoice.InvoiceID,
                    "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus,Template.Serial.InvoiceType,Company"
                );

                var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);
                var serializer = new XmlSerializer(typeof(HDon));
                var fileName = $"Invoice_{fullInvoice.InvoiceID}.xml";
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

                // 7. Log History
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