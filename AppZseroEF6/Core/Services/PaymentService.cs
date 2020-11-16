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
        Task<CustomerCart> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
    public class PaymentService : IPaymentService
    {
        private readonly ICustomerCartRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(ICustomerCartRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<CustomerCart> CreateOrUpdatePaymentIntent(string basketId)
        {
            try
            {
                StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
                CustomerCart basket = await _basketRepository.GetByIdAsync(basketId);
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
            var order = await _unitOfWork.GetRepository<Entities.Order>().GetByIdAsync(paymentIntentId);

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
            order.Status = "PaymentReceived";
            _unitOfWork.GetRepository<Entities.Order>().Update(order);
            _unitOfWork.Commit();
            return order;
        }


        //public async Task<string> PaymentIntentAsync(int logguedUser, int productId)
        //{
        //    var user = await _userService.GetUserAsync(logguedUser);
        //    var product = await _productService.GetProductAsync(productId);

        //    var amount = GetProductPriceForStripe(product);

        //    if (amount <= 0)
        //        throw new NotAllowedException("Price must to be great than 0");

        //    if (string.IsNullOrEmpty(user.StripeCustomerId))
        //    {
        //        await CreateStripeCustomerAsync(user);
        //    }

        //    var options = new PaymentIntentCreateOptions
        //    {
        //        Amount = amount,
        //        Currency = "eur",
        //        Customer = user.StripeCustomerId,
        //        Description = "Buying product " + product.Name + ". {ID: " + product.Id + "}"
        //    };

        //    var service = new PaymentIntentService();
        //    var paymentIntent = await service.CreateAsync(options);

        //    var statusInfo = "Stripe Payment Intent Requested";
        //    await CreateOrderAsync(paymentIntent.ClientSecret, user, product, amount, statusInfo);

        //    return paymentIntent.ClientSecret;
        //}

        ///// <summary>
        ///// Stripe Webhook
        ///// </summary>
        ///// <param name="paymentIntent"></param>
        ///// <returns></returns>
        //public async Task HandlePaymentIntentSucceeded(PaymentIntent paymentIntent)
        //{
        //    var clientSecret = paymentIntent.ClientSecret;

        //    var order = await GetOrderAsync(clientSecret);

        //    if (order == null)
        //    {
        //        order = new Entities.Order
        //        {
        //            ExternalId = clientSecret,
        //            Amount = paymentIntent.Amount / 100m,
        //            Status = OrderStatusEnum.SUCCED,
        //            StatusInformation = paymentIntent.ToString(),
        //            PaymentMethod = PaymentMethodEnum.STRIPE,
        //            CreatedAt = DateTime.UtcNow,
        //            ModifiedAt = DateTime.UtcNow
        //        };

        //        await _uow.OrderRepository.AddAsync(order);
        //        await _uow.CommitAsync();
        //    }
        //    else
        //    {
        //        if (order.Status == OrderStatusEnum.SUCCED)
        //            return;

        //        order.Status = OrderStatusEnum.SUCCED;
        //        order.StatusInformation = paymentIntent.Description;
        //        order.ModifiedAt = DateTime.UtcNow;
        //        await _uow.OrderRepository.UpdateAsync(order, order.Id);

        //        await HandleCoinsIncrementActionAsync(order);
        //        await _uow.CommitAsync();
        //    }
        //}

        ///// <summary>
        ///// Stripe Webhook
        ///// </summary>
        ///// <param name="paymentIntent"></param>
        ///// <returns></returns>
        //public async Task HandlePaymentIntentFailed(PaymentIntent paymentIntent)
        //{
        //    var clientSecret = paymentIntent.ClientSecret;
        //    var order = await GetOrderAsync(clientSecret);

        //    await HandlePaymentIntentNotSuccess(paymentIntent, order, OrderStatusEnum.FAILED);
        //}

        ///// <summary>
        ///// Stripe Webhook
        ///// </summary>
        ///// <param name="paymentIntent"></param>
        ///// <returns></returns>
        //public async Task HandlePaymentIntentCanceled(PaymentIntent paymentIntent)
        //{
        //    var clientSecret = paymentIntent.ClientSecret;
        //    var order = await GetOrderAsync(clientSecret);

        //    await HandlePaymentIntentNotSuccess(paymentIntent, order, OrderStatusEnum.CANCELED);
        //}

        //public async Task<StripeList<PaymentMethod>> GetStripeCustomerPaymentMethods(int userId)
        //{
        //    var user = await _userService.GetUserAsync(userId);
        //    var options = new PaymentMethodListOptions
        //    {
        //        Customer = user.StripeCustomerId,
        //        Type = "card",
        //    };

        //    var service = new PaymentMethodService();
        //    var paymentMethods = await service.ListAsync(options);

        //    return paymentMethods;
        //}

        //private async Task CreateStripeCustomerAsync(User user)
        //{
        //    var options = new CustomerCreateOptions
        //    {
        //        Email = user.Email,
        //        Name = user.FullName,
        //        Phone = user.Phone,
        //        Description = "PlaniFive user"
        //    };

        //    var service = new CustomerService();
        //    var customer = await service.CreateAsync(options);

        //    if (customer != null)
        //    {
        //        user.StripeCustomerId = customer.Id;
        //        await _uow.UserRepository.UpdateAsync(user, user.Id);
        //        await _uow.CommitAsync();
        //    }
        //}

        //private async Task HandlePaymentIntentNotSuccess(PaymentIntent paymentIntent, Entities.Order order, OrderStatusEnum orderStatus)
        //{
        //    var statusInformation = orderStatus == OrderStatusEnum.CANCELED ? paymentIntent.CancellationReason : paymentIntent.LastPaymentError?.Message;
        //    if (order == null)
        //    {
        //        order = new Entities.Order
        //        {
        //            ExternalId = paymentIntent.ClientSecret,
        //            Amount = paymentIntent.Amount / 100m,
        //            Status = orderStatus,
        //            StatusInformation = statusInformation,
        //            PaymentMethod = PaymentMethodEnum.STRIPE,
        //            CreatedAt = DateTime.UtcNow,
        //            ModifiedAt = DateTime.UtcNow
        //        };

        //        await _uow.OrderRepository.AddAsync(order);
        //        await _uow.CommitAsync();
        //    }
        //    else
        //    {
        //        order.Status = orderStatus;
        //        order.StatusInformation = statusInformation;
        //        order.ModifiedAt = DateTime.UtcNow;

        //        await _uow.OrderRepository.UpdateAsync(order, order.Id);
        //        await _uow.CommitAsync();
        //    }
        //}


        private int GetProductPriceForStripe(Entities.Product product)
        {

            return Convert.ToInt32(product.CurrentPrice * 100);


        }

        private async Task<Entities.Order> GetOrderAsync(string externalId)
        {
            var order = await _unitOfWork.GetRepository<Entities.Order>().GetByIdAsync(externalId);
            return order;
        }

        private async Task<Entities.Order> CreateOrderAsync(string clientSecret, Entities.Product product, int amount, string StatusInformation = "")
        {
            var order = new Entities.Order
            {
                //ExternalId = clientSecret,
                //UserId = user.Id,
                //UserEmail = user.Email,
                //UserFullName = user.FullName,
                //ProductId = product.Id,
                //ProductName = product.Name,
                //ProductDescription = product.Description,
                //Amount = amount / 100m,
                //Status = OrderStatusEnum.PROCESING,
                //StatusInformation = "Stripe Payment Intent Requested",
                //PaymentMethod = PaymentMethodEnum.STRIPE,
                //CreatedAt = DateTime.UtcNow,
                //ModifiedAt = DateTime.UtcNow
            };
            await _unitOfWork.GetRepository<Entities.Order>().AddAsync(order);
            await _unitOfWork.CommitAsync();
            return order;
        }
    }
}
