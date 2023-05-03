using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> Criterio { get; }
        List<Expression<Func<T, Object>>> Includes { get; }
        Expression<Func<T, Object>> OrderBy {get; }
        Expression<Func<T, Object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
