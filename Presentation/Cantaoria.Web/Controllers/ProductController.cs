using AvvaMobile.Core;
using AvvaMobile.Core.AWS;
using Cantaoria.Application.Models.Requests.ProductRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductService _productService;
        public ProductController(IProductReadRepository productReadRepository, IProductService productService)
        {
            _productService = productService;
            _productReadRepository = productReadRepository;
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
        public IActionResult Create()
        {
            var result = _productService.Create();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var result = await _productService.CreateAsync(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _productService.Update(id);
            if (!result.IsSuccess)
            {
                Message(result);
                return RedirectToAction(nameof(List));
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductRequest request)
        {
            var result = await _productService.Update(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
