using AutoMapper;
using Store.Bussines.DTOs;
using Store.Data.Entities;

namespace Store.Bussines.MappingProfiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<ClienteDto, Cliente>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));       

    }
}