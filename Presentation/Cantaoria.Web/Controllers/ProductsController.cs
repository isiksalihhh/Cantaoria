using AvvaMobile.Core.AWS;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
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
        public IActionResult ListAllData()
        {

            var result = _productReadRepository.GetAll().ToList();
            return Json(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _productWriteRepository.DeleteAsync(id);
            await _productWriteRepository.SaveAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
