using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Concrete;

namespace Cantaoria.Persistence.Repositories
{
    public class ProductPhotoReadRepository : ReadRepository<ProductPhoto>, IProductPhotoReadRepository
    {
        public ProductPhotoReadRepository(CantaoriaDbContext context) : base(context)
        {
        }
    }

}
