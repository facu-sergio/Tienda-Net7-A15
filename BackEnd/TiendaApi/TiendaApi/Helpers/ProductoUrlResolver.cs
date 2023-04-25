using AutoMapper;
using Core.Entities;
using TiendaApi.Dtos;

namespace TiendaApi.Helpers
{
    public class ProductoUrlResolver : IValueResolver<Producto, ProductoDto, string>
    {
        private readonly IConfiguration _config;

        public ProductoUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Producto source, ProductoDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.FotoUrl))
            {
               return _config["ApiUrl"] + source.FotoUrl;   
            }

            return null;
        }
    }
}
