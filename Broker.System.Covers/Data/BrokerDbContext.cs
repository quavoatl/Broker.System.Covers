using Broker.System.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Broker.System.Covers.Data
{
    public class BrokerCoversDbContext : DbContext
    {
        public BrokerCoversDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cover> Covers { get; set; }
    }
}