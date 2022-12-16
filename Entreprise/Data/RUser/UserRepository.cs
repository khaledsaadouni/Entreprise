

using Entreprise.Models;
using Microsoft.EntityFrameworkCore;

namespace Entreprise.Data.RUser
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ProjectContext context;
        public UserRepository()
        {
            this.context = ProjectContext.Instance(); ;
        }
        public IEnumerable<User> getList()
        {
            return this.context.User.Include(x=>x.Department).Include(x => x.Tasks).ToList();
        }
        public User getByID(int id)
        {
            return this.context.User.Find(id);
        }
    }
}
