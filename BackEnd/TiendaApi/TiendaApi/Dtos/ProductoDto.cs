using Core.Entities;

namespace TiendaApi.Dtos
{
    public class ProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string FotoUrl { get; set; }
        public string Tipo { get; set; }
        public string Marca { get; set; }
    }
}
