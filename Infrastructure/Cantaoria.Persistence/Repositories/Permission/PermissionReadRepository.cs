using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class PermissionReadRepository : ReadRepository<Permission>, IPermissionReadRepository
    {
        public PermissionReadRepository(CantaoriaDbContext context) : base(context)
        {
        }
    }
}
