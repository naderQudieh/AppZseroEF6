using Bridge.Data.Infrastructure;
using Bridge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bridge.Data.Repositories
{
    public interface ICategoryRepository: IRepository<Category>
    {

    }
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
