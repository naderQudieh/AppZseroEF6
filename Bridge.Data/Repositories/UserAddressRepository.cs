using Bridge.Data.Infrastructure;
using Bridge.Model;

namespace Bridge.Data.Repositories
{
   

    public interface IUserAddressRepository : IRepository<UserAddress>
    {

    }
    public class UserAddressRepository : RepositoryBase<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
