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
using EIMS.Application.DTOs.InvoiceItems;


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
                        .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(src =>
                            src.Payments != null && src.Payments.Any()
                                ? src.Payments.Sum(p => p.AmountPaid)
                                : src.PaidAmount))
                        // Ensure RemainingAmount is calculated
                        .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src =>
                            src.Payments != null && src.Payments.Any()
                                ? src.TotalAmount - src.Payments.Sum(p => p.AmountPaid)
                                : src.RemainingAmount))
                        .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src =>
        src.InvoiceCustomerName ?? (src.Customer != null ? src.Customer.CustomerName : "")))
                        .ForMember(dest => dest.OriginalInvoiceSignDate, opt => opt.MapFrom(src => 
                src.OriginalInvoice != null ? src.OriginalInvoice.SignDate : null))
           .ForMember(dest => dest.OriginalInvoiceSymbol, opt => opt.MapFrom(src =>
                (src.OriginalInvoice != null
                 && src.OriginalInvoice.Template != null
                 && src.OriginalInvoice.Template.Serial != null)
                ? src.OriginalInvoice.Template.Serial.Prefix.PrefixID
                  + src.OriginalInvoice.Template.Serial.SerialStatus.Symbol
                  + src.OriginalInvoice.Template.Serial.Year
                  + src.OriginalInvoice.Template.Serial.InvoiceType.Symbol
                  + src.OriginalInvoice.Template.Serial.Tail
                : null))
    .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src =>
        src.InvoiceCustomerAddress ?? (src.Customer != null ? src.Customer.Address : "")))
    .ForMember(dest => dest.ContactPerson, opt => opt.MapFrom(src =>
        (src.Customer != null ? src.Customer.ContactPerson : "")))
    .ForMember(dest => dest.ContactPhone, opt => opt.MapFrom(src =>
        (src.Customer != null ? src.Customer.ContactPhone : "")))
    .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => (src.Customer != null ? src.Customer.ContactEmail : "")))
                            .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src =>
                                src.InvoiceCustomerTaxCode ?? (src.Customer != null ? src.Customer.TaxCode : "")))
                                .ForMember(dest => dest.TaxStatusCode, opt => opt.MapFrom(src => 
        src.TaxApiLogs.OrderByDescending(l => l.Timestamp).FirstOrDefault() != null 
            ? src.TaxApiLogs.OrderByDescending(l => l.Timestamp).FirstOrDefault().TaxApiStatus.Code // Assuming Status has 'Code'
            : "NOT_SENT")) // Default if no logs exist
            
    .ForMember(dest => dest.TaxStatusDescription, opt => opt.MapFrom(src => 
        src.TaxApiLogs.OrderByDescending(l => l.Timestamp).FirstOrDefault() != null 
            ? src.TaxApiLogs.OrderByDescending(l => l.Timestamp).FirstOrDefault().TaxApiStatus.StatusName 
            : "Chưa gửi CQT"))
            .ForMember(dest => dest.TaxStatusColor, opt => opt.MapFrom(src =>
                    GetStatusColor(
                        src.TaxApiLogs.OrderByDescending(l => l.Timestamp)
                        .Select(l => l.TaxApiStatusID) 
                        .FirstOrDefault() 
                    )))
                    .ForMember(dest => dest.OriginalInvoiceID, opt => opt.MapFrom(src => 
    // Case 1: No Original Invoice linked -> Return NULL
    src.OriginalInvoiceID == null ? null :
    
    // Case 2: Has Original Invoice, but it hasn't been numbered yet -> Return 0
    (src.OriginalInvoice != null && src.OriginalInvoice.InvoiceNumber == null ? 0 : 
    
    // Case 3: Has Original Invoice and it is numbered -> Return the ID
    src.OriginalInvoiceID)))
                    .ForMember(dest => dest.OriginalInvoiceNumber, opt => opt.MapFrom(src => 
        src.OriginalInvoice != null ? src.OriginalInvoice.InvoiceNumber : null))
                        .ReverseMap();
                         CreateMap<CreateSerialCommand, Serial>();
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
            CreateMap<InvoiceItem, GetInvoiceItemsDTO>()
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src =>
                   src.Product != null ? src.Product.Name : "Unknown Product"))
               .ForMember(dest => dest.Unit, opt => opt.MapFrom(src =>
                   src.Product != null ? src.Product.Unit : ""))
               .ReverseMap();
            CreateMap<Customer, CustomerDto>();
            CreateMap<UpdateInvoiceRequest, UpdateInvoiceCommand>();
            CreateMap<InvoicePayment, InvoicePaymentDTO>()
                    .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src =>
                    src.Invoice != null
                    ? src.Invoice.TotalAmount - src.Invoice.Payments.Sum(p => p.AmountPaid)
                    : 0
                ));
            CreateMap<InvoicePayment, InvoicePaymentDetailDTO>()
             .ForMember(dest => dest.InvoiceCode, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.InvoiceNumber : null))
             .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Invoice != null && src.Invoice.Customer != null ? src.Invoice.Customer.CustomerName : "Khách lẻ"))
             .ForMember(dest => dest.TotalInvoiceAmount, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.TotalAmount : 0))
             .ForMember(dest => dest.TotalPaidAmount, opt => opt.MapFrom(src => src.Invoice != null ? src.Invoice.PaidAmount : 0))
             .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src => src.Invoice != null ? (src.Invoice.TotalAmount - src.Invoice.PaidAmount) : 0))

             // 4. Logic tính trạng thái (Custom Logic)
             .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => GetInvoiceStatus(src.Invoice)));
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
            CreateMap<AuditLog, AuditLogDto>()
             .ForMember(dest => dest.AuditID, opt => opt.MapFrom(src => src.AuditID))
             .ForMember(dest => dest.TraceId, opt => opt.MapFrom(src => src.TraceId))
             .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
             // Map UserName từ bảng User liên kết
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "System"));
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
        private string GetInvoiceStatus(Invoice? invoice)
        {
            if (invoice == null) return "Không xác định";
            if (invoice.PaidAmount >= invoice.TotalAmount)
            {
                return "Hoàn tất";
            }

            return "Thanh toán một phần";
        }
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
        private static string GetStatusColor(int statusId)
        {
            return statusId switch
            {
                // SUCCESS (Green)
                // 4=Approved, 10=Valid TB01, 30=Code Granted KQ01
                4 or 10 or 30 => "success", 

                // PROCESSING (Blue/Orange)
                // 1=Pending, 2=Received, 6=Processing, 32=No Result
                1 or 2 or 6 or 32 => "processing",

                // ERROR (Red)
                // 3=Rejected, 5=System Error, 9=Tech Error, 31=Code Rejected
                // 11-21 = Specific Validation Errors
                3 or 5 or 9 or 31 => "error",
                >= 11 and <= 21 => "error",

                // DEFAULT (Gray)
                // 0 (Not Sent), 7 (Not Found), 8 (Draft), 33 (Not Found KQ)
                _ => "default" 
            };
        }
    }
}


