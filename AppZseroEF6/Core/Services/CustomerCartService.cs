using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Data.Repositories;
using AppZseroEF6.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppZseroEF6.Service
{
    
    public interface ICustomerCartService
    {
        IQueryable<CustomerCart> GetCustomerCarts();
        CustomerCart GetCustomerCart(string id);
        void CreateCustomerCart(CustomerCart CustomerCart);
        void SaveChanges();
    }
    public class CustomerCartService : ICustomerCartService
    {
        private readonly ICustomerCartRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerCartService(ICustomerCartRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateCustomerCart(CustomerCart CustomerCart)
        {  
            _repository.Add(CustomerCart);
        }

        public CustomerCart GetCustomerCart(string id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<CustomerCart> GetCustomerCarts()
        {
            return _repository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
