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


namespace EIMS.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from the Request DTO to the Command
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginResponse, AuthResponse>();
            CreateMap<RefreshTokenResponse, AuthResponse>();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<CreateSerialCommand, Serial>();
            CreateMap<CreateSerialRequest, CreateSerialCommand>();
            CreateMap<CreateTemplateRequest, CreateTemplateCommand>();
            CreateMap<CreateTemplateCommand, InvoiceTemplate>();
        }
    }
}