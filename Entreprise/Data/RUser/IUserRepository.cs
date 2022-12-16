
using Entreprise.Models;

namespace Entreprise.Data.RUser
{
    public interface IUserRepository : IRepository<User>
    {
        public IEnumerable<User> getList();
        public User  getByID(int id);
    }
}
