using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace BoxStoreDAL
{
    class DataContext : DbContext
    {
        public virtual DbSet<BoxStoreModels.Box> Boxes { get; set; }
        public DataContext() : base("BoxStoreDB")
        {
            _ = SqlProviderServices.Instance;
        }
    }
}
