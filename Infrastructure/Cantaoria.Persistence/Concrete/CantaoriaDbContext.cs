using Cantaoria.Domain.Entities;
using Cantaoria.Domain.Entities.Common;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Category> Categories { get; set; }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker
                .Entries<BaseEntity>();
                
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
