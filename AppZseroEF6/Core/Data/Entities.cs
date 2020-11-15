using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppZseroEF6.Entities
{
    public class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.KeyAttribute]
        [Column("Id")] 
        public string Id { get; set; }

      

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "date_created")]
        public DateTime date_created { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "date_modified")]
        public DateTime date_modified { get; set; }
        public BaseEntity()
        {
            Id= Guid.NewGuid().ToString().ToLower().Replace("-", "");
            date_created = DateTime.Now;
            date_modified = DateTime.Now;
        }
    }
    public class CustomerCartItems : BaseEntity
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product  Product { get; set; }
    }
    public class CustomerCart : BaseEntity
    { 
         
        public List<CustomerCartItems> Items { get; set; } = new List<CustomerCartItems>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
    public class Category : BaseEntity
    { 
       
        public String Name { get; set; }
        public String Logo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    public class UserAddress : BaseEntity
    {
       
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
        public String UserId { get; set; }



    }
    public class Product : BaseEntity
    {  
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public long CategoryId { get; set; }
        public string Status { get; set; }
      
        public DateTime DateSale { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
    public class OrderDetail : BaseEntity
    {
        
        public string ProductId { get; set; }
        public string OrderId { get; set; } 
        public int Quantity { get; set; }
        public string Comment { get; set; }
        public float? Star { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
    public class Order : BaseEntity
    { 
        public String BuyerId { get; set; }
        public double TotalAmount { get; set; }
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public String  Status { get; set; } 
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
    
     
}
