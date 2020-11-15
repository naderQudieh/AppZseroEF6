using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppZseroEF6.Entities;
using AppZseroEF6.Service;
using AppZseroEF6.Util;
using AppZseroEF6.ModelsDtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PushSharp.Google;

namespace AppZseroEF6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
       
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _detailService;
        private readonly IProductService _productService;
        
        public OrderController( IOrderService orderService, IOrderDetailService detailService, IProductService productService )
        {
           
            _orderService = orderService;
            _detailService = detailService;
            _productService = productService;
            
        }

        [HttpGet]
        public async Task<ActionResult> getOrders(bool isDone = false)
        {
            int status1 = (int)OrderCurrentStatus.received;
            int status2 = (int)OrderCurrentStatus.doing;
            if (isDone)
            {
                status1 = (int)OrderCurrentStatus.cancel;
                status2 = (int)OrderCurrentStatus.done;

            }

            var orders = _orderService.GetOrders(); 
           
            List<OrderDto> result = new List<OrderDto>();
            foreach (var order in orders)
            {
                var item = order.Adapt<OrderDto>();
                result.Add(item);
            }
            return Ok(result);
        }

        [HttpGet("Admin")]
        public async Task<ActionResult> GetAll()
        {
            var orders = _orderService.GetOrders()
                .OrderByDescending(_ => _.DateCreated)
                .Select(_=>_.Adapt<OrderDto>());

            
            return Ok(orders);

        }

       

        [HttpGet("{id}")]
        public async Task<ActionResult> getOrder(string id)
        {
            var order = _orderService.GetOrder(id);
            OrderDto result = order.Adapt<OrderDto>();
            foreach (var item in order.OrderDetails)
            {
                var orderDetail = item.Adapt<OrderDetailDto>();
                
                orderDetail.ProductName = item.Product.Name;
                result.OrderDetail.Add(orderDetail);
            }
           
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> createOrder(OrderDto model)
        {
            double totalAmount = 0;
            foreach (var item in model.OrderDetail)
            {
                if (item.Quantity <=  0 || item.Quantity > 5) return BadRequest();
                var product = _productService.GetProduct(item.ProductId);
                if (product == null) return BadRequest();
                totalAmount += product.CurrentPrice * item.Quantity;
            }
            
            var order = model.Adapt<Order>();
            
            order.TotalAmount = totalAmount;
            order.Status = OrderCurrentStatus.received.ToString();
            _orderService.CreateOrder(order);
            foreach (var item in model.OrderDetail)
            {
                var orderDetail = item.Adapt<OrderDetail>();
                orderDetail.OrderId = order.Id;
                _detailService.CreateOrderDetail(orderDetail);
            }
            _orderService.SaveChanges(); 
            return Ok();
        }

        
       

        [HttpPut("{id}/Cancel")]
        public async Task<ActionResult> CancelOrder(string id)
        {
            
            var order = _orderService.GetOrders().Where(p=>p.Id == id ).FirstOrDefault();
            if (order == null) return NotFound();
            if (order.Status !=  OrderCurrentStatus.received.ToString()
                && order.Status !=  OrderCurrentStatus.doing.ToString())
                return BadRequest(); 
            _orderService.SaveChanges();
            return Ok();
        }
    }
}