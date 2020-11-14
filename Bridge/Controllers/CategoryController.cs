using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppZseroEF6.Model;
using AppZseroEF6.Service;
using AppZseroEF6.Util;
using AppZseroEF6.ViewModels;
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
            var categories = _categoryService.GetCategories().Select(c => c.Adapt<CategoryVM>());
            return Ok(categories);
        }

        [HttpGet("{id}/Product")]
        public ActionResult GetProducts(long id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null) return NotFound();
            var products = category.Products
                .Where(p => p.DateSale <= DateTime.Now && p.Status == (int)ProductStatus.available);
            List<ProductVM> result = new List<ProductVM>();
            foreach (var product in products)
            {
                ProductVM item = product.Adapt<ProductVM>();
                item.Description = "";
               
                item.Star = 4.6;
                result.Add(item);
            }
            return Ok(result);
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
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