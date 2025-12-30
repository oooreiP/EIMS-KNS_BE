using AutoMapper;
using EIMS.Application.DTOs.Authentication;
using EIMS.Application.DTOs.InvoiceTemplate;
using EIMS.Application.DTOs.Serials;
using EIMS.Application.Features.Authentication.Commands;
using EIMS.Application.Features.Commands;
using EIMS.Application.Features.InvoiceTemplate.Commands;
using EIMS.Application.Features.Serial.Commands;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.Features.Authentication.Commands;
using EIMS.Application.Features.Commands;
using EIMS.Domain.Entities;
using EIMS.Application.DTOs.User;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Application.Features.InvoiceStatements.Commands;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.Features.Invoices.Commands.UpdateInvoice;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.LogsDTO;


namespace EIMS.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginResponse, AuthResponse>();
            CreateMap<RefreshTokenResponse, AuthResponse>();
            CreateMap<Invoice, InvoiceDTO>()
                        // Map the PaymentStatus name from the navigation property
                        .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src =>
                            src.PaymentStatus != null ? src.PaymentStatus.StatusName : "Unknown"))
                        // Ensure PaidAmount is calculated from the Payments collection (which we now include)
                        .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src => src.PaidAmount))
                        // Ensure RemainingAmount is calculated
                        .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src => src.RemainingAmount))
                        .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
        src.InvoiceCustomerName ?? (src.Customer != null ? src.Customer.CustomerName : "")))

    .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src =>
        src.InvoiceCustomerAddress ?? (src.Customer != null ? src.Customer.Address : "")))

    .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src =>
        src.InvoiceCustomerTaxCode ?? (src.Customer != null ? src.Customer.TaxCode : "")))
                        .ReverseMap(); CreateMap<CreateSerialCommand, Serial>();
            CreateMap<CreateSerialRequest, CreateSerialCommand>();
            CreateMap<CreateTemplateRequest, CreateTemplateCommand>();
            CreateMap<CreateTemplateCommand, InvoiceTemplate>();
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
            CreateMap<GenerateStatementRequest, CreateStatementCommand>();
            CreateMap<UpdateTemplateRequest, UpdateTemplateCommand>();
            CreateMap<UpdateTemplateCommand, InvoiceTemplate>();
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.Name : "Unknown Product"))
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.Unit : ""))
                .ReverseMap();
            CreateMap<Customer, CustomerDto>();
            CreateMap<UpdateInvoiceRequest, UpdateInvoiceCommand>();
            CreateMap<InvoicePayment, InvoicePaymentDTO>();
            CreateMap<InvoiceStatementDetail, StatementInvoiceDto>()
                  .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.InvoiceNumber : 0))
                  .ForMember(dest => dest.SignDate, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.SignDate : null))
                  .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.TotalAmount : 0))
                  // Map the snapshot amount explicitly saved in the detail
                  .ForMember(dest => dest.OwedAmount, opt => opt.MapFrom(src => src.OutstandingAmount))
                  // Logic: If they owe less than the total, it's Partial.
                  .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src =>
                      (src.Invoice != null && src.OutstandingAmount < src.Invoice.TotalAmount) ? "Partial" : "Unpaid"));

            // Map Main Response (Parent)
            CreateMap<InvoiceStatement, StatementDetailResponse>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CustomerName : "Unknown"))
                // Map Status Name safely (handling case where navigation prop might be null)
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.StatementStatus != null ? src.StatementStatus.StatusName :
                    (src.StatusID == 5 ? "Paid" : src.StatusID == 4 ? "Partially Paid" : "Draft")))
                .ForMember(dest => dest.Invoices, opt => opt.MapFrom(src => src.StatementDetails));
            // Fix: Customer -> CustomerInfoDto
            CreateMap<Customer, CustomerInfoDto>();
            CreateMap<AuditLog, AuditLogDto>();
            CreateMap<SystemActivityLog, SystemActivityLogDto>();
            // Fix: Invoice Entity -> StatementInvoiceDto (For the invoices list)
            CreateMap<Invoice, StatementInvoiceDto>()
                  .ForMember(dest => dest.InvoiceID, opt => opt.MapFrom(src => src.InvoiceID))
                  .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.InvoiceNumber))
                  .ForMember(dest => dest.SignDate, opt => opt.MapFrom(src => src.SignDate))
                  .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                  .ForMember(dest => dest.OwedAmount, opt => opt.MapFrom(src => src.RemainingAmount))
                  .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src =>
                      src.PaymentStatus != null ? src.PaymentStatus.StatusName : "Unknown"));

            // Fix: InvoicePayment Entity -> PaymentHistoryDto (For the payments list)
            CreateMap<InvoicePayment, PaymentHistoryDto>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentID))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.AmountPaid))
                .ForMember(dest => dest.InvoiceNumber, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.InvoiceNumber.ToString() : ""))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CreatedBy));
            // Assuming you have a Creator navigation property to get the username
            // .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : "System"));
        }

        private static string GetStatusName(int id) => id switch
        {
            1 => "Draft",
            2 => "Published",
            3 => "Sent",
            4 => "Partially Paid",
            5 => "Paid",
            6 => "Cancelled",
            7 => "Refunded",
            _ => "Unknown"
        };

        private static string GetPaymentStatus(decimal owed, decimal total)
        {
            if (owed <= 0) return "Paid";
            if (owed >= total) return "Unpaid";
            return "Partially Paid";
        }

        private static long ParseInvoiceNumber(string code)
        {
            if (string.IsNullOrEmpty(code)) return 0;
            var digits = new string(code.Where(char.IsDigit).ToArray());
            return long.TryParse(digits, out var n) ? n : 0;
        }
    }
}


