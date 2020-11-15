using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int? DeliveryMethodID { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
    public class BasketItemDto
    {

        [Required]
        public string Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
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
