using AppZseroEF6.Entities;
using AppZseroEF6.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AppZseroEF6.Data.Infrastructure;

namespace AppZseroEF6.Data.Repositories
{
    
    public interface IBasketRepository : IRepository<CustomerBasket>
    {

    }
    public class  BasketRepository : RepositoryBase<CustomerBasket>, IBasketRepository
    {
        public BasketRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
}
