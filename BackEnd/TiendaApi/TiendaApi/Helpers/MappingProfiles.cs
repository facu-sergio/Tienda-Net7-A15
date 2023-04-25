using AutoMapper;
using Core.Entities;
using TiendaApi.Dtos;

namespace TiendaApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>()
                   .ForMember(d => d.Marca, o => o.MapFrom(s => s.Marca.Nombre))
                   .ForMember(d => d.Tipo, o => o.MapFrom(s => s.Tipo.Nombre))
                   .ForMember(d => d.FotoUrl, o => o.MapFrom<ProductoUrlResolver>());
        }
    }
}
