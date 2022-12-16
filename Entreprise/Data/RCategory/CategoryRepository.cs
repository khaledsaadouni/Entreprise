using Entreprise.Models;

namespace Entreprise.Data.RCategory
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ProjectContext context;
        public CategoryRepository() {
            this.context = ProjectContext.Instance(); ;
        }
    }
}
