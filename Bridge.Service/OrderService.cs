using Bridge.Data.Infrastructure;
using Bridge.Data.Repositories;
using Bridge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.Service
{
    
    public interface IOrderService
    {
        IQueryable<Order> GetOrders();
        Order GetOrder(long id);
        void CreateOrder(Order order);
        void SaveChanges();
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateOrder(Order order)
        {
            order.DateCreated = DateTime.Now;
            _repository.Add(order);
        }

        public Order GetOrder(long id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Order> GetOrders()
        {
            return _repository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
