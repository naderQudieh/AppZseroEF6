using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppZseroEF6.Service;
using AppZseroEF6.Util;
using AppZseroEF6.ModelsDtos;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppZseroEF6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var categories = _categoryService.GetCategories().Select(c => c.Adapt<CategoryDto>());
            return Ok(categories);
        }

        [HttpGet("{id}/Product")]
        public ActionResult GetProducts(long id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null) return NotFound();
            var products = category.Products
                .Where(p => p.DateSale <= DateTime.Now && p.Status == (int)ProductStatus.available);
            List<ProductDto> result = new List<ProductDto>();
            foreach (var product in products)
            {
                ProductDto item = product.Adapt<ProductDto>();
                item.Description = "";
               
                item.Star = 4.6;
                result.Add(item);
            }
            return Ok(result);
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryDto category)
        { 
            _categoryService.CreateCategory(category);
            _categoryService.SaveChanges();
            return StatusCode(
                201, new
                {
                    Id = category.Id
                });
        }

    }
}