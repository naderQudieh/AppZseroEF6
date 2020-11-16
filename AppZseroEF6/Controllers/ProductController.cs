using AppZseroEF6.Entities;
using AppZseroEF6.Service;
using AppZseroEF6.ModelsDtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppZseroEF6.Util;
namespace AppZseroEF6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderDetailService _orderDetailService;

        public ProductController(IProductService productService, IOrderDetailService orderDetailService)
        {
            _productService = productService;
            _orderDetailService = orderDetailService;
        }

        [HttpGet("New")]
        public ActionResult GetNewBrand()
        {
            var productNewBard = _productService.GetProducts()
                    .Where(p => p.DateSale <= DateTime.Now && p.Status == ProductStatus.available.ToString())
                    .OrderByDescending(p => p.DateSale).Take(5);
            List<ProductDto> result = new List<ProductDto>();
            foreach(var product in productNewBard)
            {
                ProductDto item = product.Adapt<ProductDto>();
                item.Description = "";
      
                
                result.Add(item);
            }
            return Ok(result);
        }

        [HttpGet("Explore")]
        public ActionResult GetNewBrandExplore()
        {
            var productNewBard = _productService.GetProducts()
                    .Where(p => p.DateSale <= DateTime.Now && p.Status ==  ProductStatus.available.ToString())
                    .OrderByDescending(p => p.DateSale).Skip(5).Take(5);
            List<ProductDto> result = new List<ProductDto>();
            foreach (var product in productNewBard)
            {
                ProductDto item = product.Adapt<ProductDto>();
                item.Description = "";
                 
                float? star = product.OrderDetails.Sum(_ => _.Star);
                if (star != null && star > 0)
                {
                    item.Star = star.Value / product.OrderDetails.Count;
                }
                else
                {
                    item.Star = 5.0;
                }
                result.Add(item);
            }
            return Ok(result);
        }

        [HttpGet()]
        public ActionResult Gets(String name)
        {
            name = name == null ? "" : name;
            var products = _productService.GetProducts()
                    .Where(p => p.DateSale <= DateTime.Now
                                && p.Status ==  ProductStatus.available.ToString()
                                && p.Name.ToLower().Contains(name.ToLower())
                                )
                    .OrderByDescending(p => p.DateSale);
            List<ProductDto> result = new List<ProductDto>();
            foreach (var product in products)
            {
                ProductDto item = product.Adapt<ProductDto>();
                item.Description = "";
                
                float? star = product.OrderDetails.Sum(_ => _.Star);
                if (star != null && star > 0)
                {
                    item.Star = star.Value / product.OrderDetails.Count;
                }
                else
                {
                    item.Star = 5.0;
                }
                result.Add(item);
            }
            return Ok(result);
        }


        /// <summary>
        /// Admin
        /// </summary>
        [HttpGet("Admin")]
        public ActionResult AdminGets()
        {
            var products = _productService.GetProducts().OrderByDescending(p => p.DateSale);
            List<ProductDto> result = new List<ProductDto>();
            foreach (var product in products)
            {
                ProductDto item = product.Adapt<ProductDto>();
                
                float? star = product.OrderDetails.Sum(_ => _.Star);
                if(star != null && star > 0)
                {
                    item.Star = star.Value / product.OrderDetails.Count;
                }
                else
                {
                    item.Star = 5.0;
                }
                result.Add(item);
            }
            return Ok(result);
        }

        [HttpPost("Admin")]
        public ActionResult AdminPost(ProductDto model)
        {
            var product = model.Adapt<Product>();
            product.Status =  ProductStatus.available.ToString();
            
            _productService.CreateProduct(product);
            _productService.SaveChanges();
            return StatusCode(201, new
            {
                Id =  product.Id
            }) ;
        }

        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            var product = _productService.GetProduct(id);
            if (product == null) return NotFound();
            ProductDto result = product.Adapt<ProductDto>();
            
            float? star = product.OrderDetails.Sum(_ => _.Star);
            if (star != null && star > 0)
            {
                result.Star = star.Value / product.OrderDetails.Count;
            }
            else
            {
                result.Star = 5.0;
            }
            return Ok(result);
        }

         
         

        [HttpPut("Admin")]
        public ActionResult AdminPut(ProductDto model)
        {
            var product = _productService.GetProduct(model.Id);
            if (product == null) return NotFound();
            product = model.Adapt(product);
            
           
            _productService.SaveChanges();
            return StatusCode(201, new
            {
                Id = product.Id
            });
        }

        [HttpDelete("Admin/{id}")]
        public ActionResult AdminDelete(string id)
        {
            var product = _productService.GetProduct(id);
            if (product == null) return NotFound();

            _productService.DeleteProduct(product);
            return Ok();
        }
    }
}
