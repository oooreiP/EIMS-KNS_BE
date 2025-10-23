using AutoMapper;
using EIMS.Application.DTOs;
using EIMS.Application.Features.Authentication.Commands;
using EIMS.Domain.Entities;

namespace EIMS.Application.Common
{
    public class MappingProfile : Profile
    {
        //Map user => authResponse DTO
        public MappingProfile()
        {
            //map user => authResponse
            CreateMap<Users, AuthResponse>()
                .ForMember(dest => dest.Role,
                        opt => opt.MapFrom(src => src.Role.ToString())) //map enum to string
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore()) //ignore to set manuallu
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore()); //ignore to set manually
        }
    }
}