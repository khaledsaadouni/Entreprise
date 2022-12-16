

using Entreprise.Models;

namespace Entreprise.Data.RVendor
{
    public class VendorRepository : Repository<Vendor>, IVendorRepository
    {
        private readonly ProjectContext context;
        public VendorRepository()
        {
            this.context = ProjectContext.Instance(); ;
        }
    }
}
