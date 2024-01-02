using Microsoft.EntityFrameworkCore;

namespace Cantaoria.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}
