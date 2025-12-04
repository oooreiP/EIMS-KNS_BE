using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<Invoice>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<Invoice>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Items == null || !request.Items.Any())
                    return Result.Fail("Invoice must contain at least one item.");
                Customer? customer = null;
                if (request.CustomerID == null || request.CustomerID == 0)
                {
                    customer = new Customer
                    {
                        CustomerName = request.CompanyName ?? request.Name ?? "Khách hàng chưa đặt tên",
                        TaxCode = request.TaxCode ?? "",
                        Address = request.Address ?? "Chưa cập nhật",
                        ContactPerson = request.Name,
                        ContactPhone = ""
                    };

                    customer = await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);
                }
                var nextInvoiceNumber = await _unitOfWork.InvoicesRepository.GetNextInvoiceNumberAsync(request.TemplateID ?? 1);
                decimal subtotal = request.Items.Sum(i => i.Amount);
                decimal vatAmount = request.Items.Sum(i => i.VATAmount);

                if (request.Amount <= 0) request.Amount = subtotal;
                if (request.TaxAmount <= 0) request.TaxAmount = vatAmount;
                if (request.TotalAmount <= 0) request.TotalAmount = request.Amount + request.TaxAmount;

                decimal vatRate = (request.Amount > 0 && request.TaxAmount > 0)
                    ? Math.Round((request.TaxAmount / request.Amount) * 100, 2)
                    : 0;

                var invoice = new Invoice
                {
                    InvoiceNumber = long.Parse(nextInvoiceNumber),
                    TemplateID = request.TemplateID ?? 1,
                    CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                    CreatedAt = DateTime.UtcNow,
                    SubtotalAmount = request.Amount,
                    VATAmount = request.TaxAmount,
                    VATRate = vatRate,
                    TotalAmount = request.TotalAmount,
                    TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(request.TotalAmount),
                    InvoiceStatusID = 1,
                    IssuerID = request.SignedBy,
                    InvoiceItems = request.Items.Select(i => new InvoiceItem
                    {
                        ProductID = i.ProductId,
                        Quantity = i.Quantity,
                        Amount = i.Amount,
                        VATAmount = i.VATAmount
                    }).ToList()
                };
                await _unitOfWork.InvoicesRepository.CreateInvoiceAsync(invoice);
                await _unitOfWork.SaveChanges();

                var history = new InvoiceHistory
                {
                    InvoiceID = invoice.InvoiceID,
                    ActionType = "Created",         
                    ReferenceInvoiceID = null,
                    Date = DateTime.UtcNow,
                    PerformedBy = request.SignedBy,
                };

                await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
                await _unitOfWork.SaveChanges();

                var fullInvoice = await _unitOfWork.InvoicesRepository
                    .GetByIdAsync(invoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
                var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);

                var serializer = new XmlSerializer(typeof(HDon));
                var fileName = $"Invoice_{fullInvoice.InvoiceNumber}.xml";
                var xmlPath = Path.Combine(Path.GetTempPath(), fileName);
                await using (var fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, xmlModel);
                }

                await using var xmlStream = File.OpenRead(xmlPath);
                var uploadResult = await _fileStorageService.UploadFileAsync(xmlStream, Path.GetFileName(xmlPath), "invoices");

                if (uploadResult.IsFailed)
                    return Result.Fail(uploadResult.Errors);

                fullInvoice.XMLPath = uploadResult.Value.Url;
                await _unitOfWork.InvoicesRepository.UpdateAsync(fullInvoice);
                await _unitOfWork.SaveChanges();

                return Result.Ok(invoice);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message).CausedBy(ex));
            }
        }
    }
}