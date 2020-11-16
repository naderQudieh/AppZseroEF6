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
        public string CategoryId { get; set; }
        public string Status { get; set; }
      
        public DateTime DateSale { get; set; }
       // [ForeignKey("CategoryId")]
       
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

   
    public class User2  : BaseEntity
    {

        public long user_id { get; set; }

        [Column("username")]
        [JsonProperty("username")]
        public string username { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        [JsonIgnore]
        public string password { get; set; }
        [JsonIgnore]
        public string password_hash { get; set; }

        [JsonIgnore]
        public string password_salt { get; set; }
        public string role { get; set; }


        [NotMapped]
        public string verification_token { get; set; }
        [NotMapped]
        public DateTime? date_verified { get; set; }


        public DateTime date_created { get; set; }


        public DateTime date_modified { get; set; }

        [Column("language")]
        public int language { get; set; }

        [Column("profile_picture")]
        public string profile_picture { get; set; }


    }

    [Table("usertoken")]
    public class UserToken
    {
        public int Id { get; set; }

        public string AccessToken { get; set; }

        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        public string RefreshToken { get; set; }

        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        public int UserId { get; set; }
        public string DeviceBrand { get; set; }
        public string DeviceModel { get; set; }
        public string OS { get; set; }
        public string OSPlatform { get; set; }
        public string OSVersion { get; set; }
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string ClientVersion { get; set; }
        public virtual User User { get; set; }
    }

    [Table("user")]
    public class User
    {
        public User()
        {
            UserTokens = new HashSet<UserToken>(); 
            Orders = new HashSet<Order>(); 
        }

        public int Id { get; set; }
        public string StripeCustomerId { get; set; }
        public string PaypalCustomerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }

        public string Phone { get; set; }

        public string Status { get; set; }

        public DateTime? LastLoggedIn { get; set; }
        public string Avatar { get; set; }
        public string AvatarMimeType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? ActivatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }
        public bool MarkedForDeletion { get; set; }
        public int VerificationCode { get; set; }
        public bool TermsAndConditionsAccepted { get; set; }
        public string TimeZone { get; set; }
        public int TimeZoneOffset { get; set; }
        public double CurrentImc { get; set; } 
        public virtual ICollection<UserToken> UserTokens { get; set; } 
        public virtual ICollection<Order> Orders { get; set; }
    }
}
