using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Data.Repositories
{
    

    public interface IOrderRepository : IRepository<Order>
    {

    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
}
