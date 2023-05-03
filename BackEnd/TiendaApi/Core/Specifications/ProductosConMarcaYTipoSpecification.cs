using Core.Entities;
namespace Core.Specifications
{
    public class ProductosConMarcaYTipoSpecification : BaseSpecification<Producto>
    {
        public ProductosConMarcaYTipoSpecification(ProductSpecParams productParams)
           : base(x =>
           (string.IsNullOrEmpty(productParams.Search) || x.Nombre.ToLower().Contains(productParams.Search)) &&
           (!productParams.BrandId.HasValue || x.MarcaId == productParams.BrandId) &&
           (!productParams.TypeId.HasValue || x.TipoId == productParams.TypeId)
           )
        {
            AddInclude(x => x.Tipo);
            AddInclude(x => x.Marca);
            AddOrderby(x => x.Nombre);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1),
                productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderby(p => p.Precio);
                        break;
                    case "priceDesc":
                        AddOrderbyDescending(p => p.Precio);
                        break;
                    default:
                        AddOrderby(n => n.Nombre);
                        break;
                }
            }
        }
        public ProductosConMarcaYTipoSpecification(int id) : base (x=> x.Id == id)
        {
            AddInclude(P => P.Marca);
            AddInclude(P => P.Tipo);
        }
    }
}
