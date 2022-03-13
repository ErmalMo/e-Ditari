using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Nxenesi, Nxenesi>();
            CreateMap<Ankes , Ankes>();
            CreateMap<Book , Book>();
            CreateMap<Note , Note>();
            CreateMap<Sesion , Sesion>();
        }
    }
}