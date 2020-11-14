using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Entities;

namespace AppZseroEF6.Data.Repositories
{
   

    public interface IUserAddressRepository : IRepository<UserAddress>
    {

    }
    public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(AppZerobDbContext DbContext) : base(DbContext)
        {

            dataContext = DbContext;
        }
    }
}
