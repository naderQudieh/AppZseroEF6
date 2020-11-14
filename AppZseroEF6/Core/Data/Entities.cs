using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppZseroEF6.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String Name { get; set; }
        public String Logo { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    public class UserAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
        public String UserId { get; set; }



    }
    public class Product
    {
        public Product()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public long CategoryId { get; set; }
        public int Status { get; set; }
        public long GenderId { get; set; }
        [ForeignKey("GenderId")]

        public DateTime DateSale { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public String Size { get; set; }
        public String Smell { get; set; }
        public int Quantity { get; set; }
        public String Comment { get; set; }
        public float? Star { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public String BuyerId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime DateCreated { get; set; }
        public String Receiver { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public int CurrentStatus { get; set; }
        [ForeignKey("BuyerId")]

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
    
     
}
