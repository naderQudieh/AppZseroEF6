using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppZseroEF6.Data.Infrastructure
{

    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        public   AppZerobDbContext dataContext;
        public   DbSet<T> DbSet;

        public RepositoryBase(AppZerobDbContext context)
        {
            dataContext = context;
            DbSet = dataContext.Set<T>();
        }
        protected AppZerobDbContext DbContext
        {
            get { return dataContext; }
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAll();
            foreach (var includeProperty in includeProperties) query = query.Include(includeProperty);
            return query;
        }

        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual IQueryable<T> Where(params Expression<Func<T, bool>>[] predicates)
        {
            var query = GetAll();
            foreach (var predicate in predicates) query = query.Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            var dbEntityEntry = dataContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            var dbEntityEntry = dataContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached) 
                DbSet.Attach(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var dbEntityEntry = dataContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
    
}
