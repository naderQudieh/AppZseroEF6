using System.Linq;
using Bridge.Model;
using Bridge.Data.Repositories;
using Bridge.Data.Infrastructure;

namespace Bridge.Service
{
   
    public interface IUserAddressService
    {
        IQueryable<UserAddress> GetUserAddresss();
        UserAddress GetUserAddress(long id);
        void CreateUserAddress(UserAddress UserAddress);
        void SaveChanges();
    }
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddressRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserAddressService(IUserAddressRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateUserAddress(UserAddress UserAddress)
        {
            _repository.Add(UserAddress);
        }

        public UserAddress GetUserAddress(long id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<UserAddress> GetUserAddresss()
        {
            return _repository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
