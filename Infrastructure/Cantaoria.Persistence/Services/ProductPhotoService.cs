using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cantaoria.Persistence.Services
{
    public class ProductPhotoService : BaseService, IProductPhotoService
    {
        public ProductPhotoService(IHttpContextAccessor httpContext) : base(httpContext)
        {
        }
    }
}
