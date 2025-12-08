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
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
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
            CreateMap<Customer, CustomerDto>();
            CreateMap<UpdateInvoiceRequest, UpdateInvoiceCommand>();

        }

    }
}