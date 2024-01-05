using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class ProductPhotoWriteRepository : WriteRepository<ProductPhoto>, IProductPhotoWriteRepository
    {
        public ProductPhotoWriteRepository(CantaoriaDbContext context) : base(context)
        {
        }
    }

}
