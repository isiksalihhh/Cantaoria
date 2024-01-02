using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(CantaoriaDbContext context) : base(context)
        {
        }
    }
}
