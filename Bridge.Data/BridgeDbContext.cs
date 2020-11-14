using Bridge.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bridge.Data
{
    public class BridgeDbContext : IdentityDbContext 
    {
        public BridgeDbContext() : base((new DbContextOptionsBuilder())
            .UseLazyLoadingProxies()
            //.UseSqlServer(@"Server=.;Database=Bridge;user id=sa;password=;Trusted_Connection=True;Integrated Security=false;")
            .UseSqlServer(@"Data Source=localhost;Initial Catalog=shop-db2;Integrated Security=True;MultipleActiveResultSets=True;")
            //.UseSqlServer(@"Server=tcp:dongtv.database.windows.net;Database=Bridge;user id=dongtv;password=zaq@123123;Trusted_Connection=True;Integrated Security=false;")
            .Options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
      
      
     
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
       
        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            AutoAddData(builder);
        }

        private void AutoAddData(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Giảm Mỡ",
                    Logo = "files/images/categories/giam-mo.png"
                }, 
                new Category
                {
                    Id = 2,
                    Name = "Phục hồi",
                    Logo = "files/images/categories/phuc-hoi.png"
                }, 
                new Category
                {
                    Id = 3,
                    Name = "Sinh lý",
                    Logo = "files/images/categories/sinh-ly.png"
                }, 
                new Category
                {
                    Id = 4,
                    Name = "Tăng cân & cơ",
                    Logo = "files/images/categories/tang-can-co.png"
                }, 
                new Category
                {
                    Id = 5,
                    Name = "Tăng cơ",
                    Logo = "files/images/categories/tang-co.png"
                }, 
                new Category
                {
                    Id = 6,
                    Name = "Tăng sức",
                    Logo = "files/images/categories/tang-suc.png"
                }
                );
            
            
        }
        public void Commit()
        {
            base.SaveChanges();
        }

    }
}
