

using Entreprise.Models;

namespace Entreprise.Data.RStock
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly ProjectContext context;
        public StockRepository()
        {
            this.context = ProjectContext.Instance(); ;
        }
    }
}
