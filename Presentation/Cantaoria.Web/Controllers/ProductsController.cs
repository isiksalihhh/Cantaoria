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
        public  IActionResult List()
        {

            return View();
        }
    }
}
