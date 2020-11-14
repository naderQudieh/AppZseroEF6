using Bridge.Data.Infrastructure;
using Bridge.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Data.Repositories
{
    

    public interface IOrderRepository : IRepository<Order>
    {

    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
