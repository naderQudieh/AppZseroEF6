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
    
    public interface ICustomerCartRepository : IRepository<CustomerCart>
    {

    }
    public class  CustomerCartRepository : RepositoryBase<CustomerCart>, ICustomerCartRepository
    {
        public CustomerCartRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
}
