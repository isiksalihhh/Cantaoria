using Cantaoria.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
 
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new(){ID = Guid.NewGuid(), Name = "Product 1", Price = 10, CreatedDate= DateTime.UtcNow, Stock = 10},
                new(){ID = Guid.NewGuid(), Name = "Product 2", Price = 15, CreatedDate= DateTime.UtcNow, Stock = 6},
                new(){ID = Guid.NewGuid(), Name = "Product 3", Price = 12, CreatedDate= DateTime.UtcNow, Stock = 8},
                new(){ID = Guid.NewGuid(), Name = "Product 4", Price = 11, CreatedDate= DateTime.UtcNow, Stock = 1},

                });
            await _productWriteRepository.SaveAsync();

            return View();
        }
    }
}
