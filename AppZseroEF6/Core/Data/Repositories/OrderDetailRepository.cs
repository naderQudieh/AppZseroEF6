using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppZseroEF6.Data.Repositories
{
    

    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {

    }
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            this.dataContext = DbContext;
        }
    }
}
