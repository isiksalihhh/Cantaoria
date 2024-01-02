using Cantaoria.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cantaoria.Persistence.Concrete
{
    public class CantaoriaDbContext : DbContext
    {

        public CantaoriaDbContext(DbContextOptions<CantaoriaDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
