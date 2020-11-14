using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Data.Repositories;
using AppZseroEF6.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppZseroEF6.Service
{
  
    public interface IOrderDetailService
    {
        IQueryable<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetail(long id);
        void CreateOrderDetail(OrderDetail OrderDetail);
        void UpdateOrderDetail(OrderDetail OrderDetail);
        void SaveChanges();
    }
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailService(IOrderDetailRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateOrderDetail(OrderDetail OrderDetail)
        {
            _repository.Add(OrderDetail);
        }

        public OrderDetail GetOrderDetail(long id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<OrderDetail> GetOrderDetails()
        {
            return _repository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateOrderDetail(OrderDetail OrderDetail)
        {
            _repository.Update(OrderDetail);
        }
    }
}
