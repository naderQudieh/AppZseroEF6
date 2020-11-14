using Bridge.Data.Infrastructure;
using Bridge.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
   
}
