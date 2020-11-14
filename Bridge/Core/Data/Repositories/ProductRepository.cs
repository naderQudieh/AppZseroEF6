using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
   
}
