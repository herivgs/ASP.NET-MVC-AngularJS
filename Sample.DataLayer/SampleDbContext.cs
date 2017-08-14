using Coqueta.Types;
using System.Data.Entity;

namespace Coqueta.DataLayer
{
    class SampleDbContext : DbContext
    {
        public SampleDbContext(string connectionString) : base(connectionString)
        {
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
