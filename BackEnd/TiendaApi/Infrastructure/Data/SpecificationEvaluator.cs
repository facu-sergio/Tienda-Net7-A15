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
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //funcion para agregar los include a la query
            query =  spec.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;   
        }
    }
}
