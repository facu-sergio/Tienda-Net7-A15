using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criterio { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criterio)
        {
            Criterio = criterio;
        }   

        protected void AddInclude (Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression); 
        }

        protected void AddOrderby (Expression<Func<T, Object>> orderbyExpression)
        {
            OrderBy = orderbyExpression;
        }

        protected void AddOrderbyDescending(Expression<Func<T, Object>> orderbyDescExpression)
        {
           OrderByDescending = orderbyDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
