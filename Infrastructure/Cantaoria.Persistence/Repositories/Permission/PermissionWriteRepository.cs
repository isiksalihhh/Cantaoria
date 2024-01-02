using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class PermissionWriteRepository : WriteRepository<Permission>, IPermissionWriteRepository
    {
        public PermissionWriteRepository(CantaoriaDbContext context) : base(context)
        {
        }
    }
}
