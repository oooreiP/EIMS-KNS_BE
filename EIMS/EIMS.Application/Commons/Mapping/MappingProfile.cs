using AutoMapper;
using EIMS.Application.DTOs.Authentication;
using EIMS.Application.Features.Authentication.Commands;
using EIMS.Application.Features.Commands;


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
        }
    }
}