using Bridge.Data.Infrastructure;
using Bridge.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Data.Repositories
{
    

    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {

    }
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
