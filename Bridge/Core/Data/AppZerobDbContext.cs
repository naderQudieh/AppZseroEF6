using AppZseroEF6.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppZseroEF6.Data
{
    public class AppZerobDbContext :  DbContext
    {
        public AppZerobDbContext(DbContextOptions<AppZerobDbContext> options)
                : base(options)
        { }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
      
      
     
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
       
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder); 
        }

        
        public void Commit()
        {
            base.SaveChanges();
        }

    }
}
