using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppZseroEF6.Service;
using AppZseroEF6.Util;
using AppZseroEF6.Entities ;
using AppZseroEF6.ModelsDtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppZseroEF6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCartController : ControllerBase
    {
        private readonly ICustomerCartService _CustomerCartService;

        public CustomerCartController(ICustomerCartService CustomerCartService)
        {
            _CustomerCartService = CustomerCartService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var categories = _CustomerCartService.GetCustomerCarts().Select(c => c.Adapt<CustomerCartDto>());
            return Ok(categories);
        }

        [HttpGet("{id}/Product")]
        public ActionResult GetProducts(string id)
        {
            var CustomerCart = _CustomerCartService.GetCustomerCart(id);
            if (CustomerCart == null) return NotFound();
            var cartItems = CustomerCart.Items.Where(p =>  p.Product.Status ==  ProductStatus.available.ToString());
            List<ProductDto> result = new List<ProductDto>();
            foreach (var item in  cartItems)
            {
                ProductDto _item = item.Product.Adapt<ProductDto>();
                _item.Description   = ""; 
                result.Add(_item);
            }
            return Ok(result);
        }
        [HttpPost]
        public ActionResult CreateCustomerCart(CustomerCartDto CustomerCart)
        {
            CustomerCart item = CustomerCart.Adapt<CustomerCart>();
            _CustomerCartService.CreateCustomerCart(item);
            _CustomerCartService.SaveChanges();
            return StatusCode(
                201, new
                {
                    Id = CustomerCart.Id
                });
        }

    }
}