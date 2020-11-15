using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppZseroEF6.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates);

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Delete(IEnumerable<T> entities);
    }
}
