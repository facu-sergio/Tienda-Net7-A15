using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery, ISpecification<T> spec)
        {
            var query = inputquery;

            if(spec.Criterio != null)
            {
                query = inputquery.Where(spec.Criterio);
            }

            //funcion para agregar los include a la query
            query =  spec.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;   
        }
    }
}
