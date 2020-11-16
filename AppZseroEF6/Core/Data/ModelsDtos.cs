using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.CustomXmlSchemaReferences;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.JsonPatch.Operations;
using System.Runtime.Serialization;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppZseroEF6.ModelsDtos
{
 

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }



        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate",
                _ => null
            };
        }
    }
    public class CustomerCartDto
    {
        [BindNever]
        [JsonIgnore]
        [Required]
        public string Id { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public List<CustomerCartItemDto> Items { get; set; } = new List<CustomerCartItemDto>();
        public int? DeliveryMethodID { get; set; }
        public string ClientSecret { get; set; }
        [JsonIgnore]
        public string PaymentIntentId { get; set; }
     
        public decimal ShippingPrice { get; set; }
    }
    public class CustomerCartItemDto
    {

        [Required]
        public string Id { get; set; }
     
        [Required]
        public string ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

       
        public ProductDto Product  { get; set; }

    }
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
        public string Id { get; set; }
        public string ProductId { get; set; }
        public String ProductName { get; set; }
        public String Size { get; set; }
        public String Smell { get; set; }
        public int Quantity { get; set; }
        public String Image { get; set; }
    }
    public class ProductDto : BaseEntity
    { 
       
        public String Name { get; set; }
        public String Description { get; set; }
        public double CurrentPrice { get; set; }
        public double OldPrice { get; set; }
        public bool IsSale { get; set; }
        public String BannerPath { get; set; }
        public double Star { get; set; }
        public string CategoryId { get; set; }

    }

    public class AddressDto
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public bool IsHome { get; set; }
    }

    public class BaseEntity
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public string Id { get; set; }


        [IgnoreDataMember]
        [JsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "date_created")]
        public DateTime date_created { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "date_modified")]
        public DateTime date_modified { get; set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            date_created = DateTime.Now;
            date_modified = DateTime.Now;
        }
    }
}
