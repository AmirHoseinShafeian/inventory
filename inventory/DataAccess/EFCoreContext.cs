using inventory.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventory.DataAccess
{
    public class EFCoreContext: DbContext
    {
        public EFCoreContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductGroup> productGroups { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductRegistration> productRegistrations { get; set; }
        public DbSet<ProductRemittance> productRemittances { get; set; }


    }
}
