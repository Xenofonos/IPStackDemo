using AutoMapper;
using IPStackLibrary.Models;

namespace IPStack.API.Profiles
{
    public class IPDetailsProfile : Profile
    {
        public IPDetailsProfile()
        {
            CreateMap<Entities.IPDetailsEntity, IPDetails>();
            CreateMap<IPDetails, Entities.IPDetailsEntity>();
        }
    }
}
