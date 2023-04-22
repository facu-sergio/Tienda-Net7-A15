using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Producto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string FotoUrl { get; set; }
        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }
        public Marca Marca { get; set; }
        public int MarcaId { get; set; }
    }
}
