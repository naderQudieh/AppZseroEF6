using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Data.Repositories;
using AppZseroEF6.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = AppZseroEF6.Entities.Order;

namespace AppZseroEF6.Service
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            try
            {
                StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
                var basket = await _basketRepository.GetByIdAsync(basketId);
                if (basket == null)
                {
                    return null;
                }

                var shippingPrice = 0m;
                 
                var service = new PaymentIntentService();
                PaymentIntent intent;
                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                    var options = new PaymentIntentCreateOptions
                    {
                        Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                        Currency = "usd",
                        PaymentMethodTypes = new List<string> { "card" }
                    };

                    intent = await service.CreateAsync(options);
                    basket.PaymentIntentId = intent.Id;
                    basket.ClientSecret = intent.ClientSecret;
                }
                else
                {
                    var options = new PaymentIntentUpdateOptions
                    {
                        Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    };

                    await service.UpdateAsync(basket.PaymentIntentId, options);
                }

                  _basketRepository.Update(basket);
                return basket;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<Entities.Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            //var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await  _unitOfWork.GetRepository<Entities.Order>().GetByIdAsync(paymentIntentId);

            if (order == null)
            {
                return null;
            }

            order.Status = "PaymentFailed";
            _unitOfWork.GetRepository<Entities.Order>().Update(order);
            _unitOfWork.Commit();
            return order;

        }

        public async Task<Entities.Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        { 
            var order = await _unitOfWork.GetRepository<Entities.Order>().GetByIdAsync(paymentIntentId);
            if (order == null)
            {
                return null;
            } 
            order.Status ="PaymentReceived";
            _unitOfWork.GetRepository<Entities.Order>().Update(order); 
            _unitOfWork.Commit();
            return order;
        }
    }
}
