using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AppZseroEF6.Data.Repositories
{
    public interface ICategoryRepository: IRepository<Category>
    {

    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
}
