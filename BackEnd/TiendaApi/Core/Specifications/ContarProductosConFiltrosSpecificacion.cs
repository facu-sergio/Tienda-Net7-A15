using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ContarProductosConFiltrosSpecificacion : BaseSpecification<Producto>
    {
        public ContarProductosConFiltrosSpecificacion(ProductSpecParams productParams) : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Nombre.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.MarcaId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.TipoId == productParams.TypeId))
        {

        }

    }
}
