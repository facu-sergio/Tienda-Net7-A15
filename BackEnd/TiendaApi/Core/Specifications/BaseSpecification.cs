using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criterio)
        {
            Criterio = criterio;
        }   

        public Expression<Func<T, bool>> Criterio { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>> ();
        
        public void AddInclude (Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression); 
        }
    }
}
