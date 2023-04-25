using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductosConMarcaYTipoSpecification: BaseSpecification<Producto>  
    {
        public ProductosConMarcaYTipoSpecification()
        {
            AddInclude(P => P.Marca);
            AddInclude(P => P.Tipo);
        }

        public ProductosConMarcaYTipoSpecification(int id) : base (x=> x.Id == id)
        {
            AddInclude(P => P.Marca);
            AddInclude(P => P.Tipo);
        }
    }
}
