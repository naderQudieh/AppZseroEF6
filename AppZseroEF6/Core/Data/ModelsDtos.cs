using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppZseroEF6.ModelsDtos
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Logo { get; set; }
    }
    public class OrderDto
    {
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public List<OrderDetailDto> OrderDetail { get; set; }

    }
    public class OrderDetailDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public String ProductName { get; set; }
        public String Size { get; set; }
        public String Smell { get; set; }
        public int Quantity { get; set; }
        public String Image { get; set; }
    }
    public class ProductDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public String BannerPath { get; set; }
        public double Star { get; set; }
        public long CategoryId { get; set; }

    }

    public class AddressDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
    }

    
}
