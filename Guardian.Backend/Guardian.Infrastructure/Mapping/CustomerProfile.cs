using AutoMapper;
using Guardian.Domain.Entities;
using Guardian.Infrastructure.ViewModel;

namespace Guardian.Infrastructure.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
