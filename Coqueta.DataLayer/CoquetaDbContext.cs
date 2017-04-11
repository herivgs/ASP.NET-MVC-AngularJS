using Coqueta.Types;
using System.Data.Entity;

namespace Coqueta.DataLayer
{
    class CoquetaDbContext : DbContext
    {
        public CoquetaDbContext(string connectionString) : base(connectionString)
        {
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
