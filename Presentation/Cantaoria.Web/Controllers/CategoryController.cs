using AvvaMobile.Core;
using Cantaoria.Application.Models.Requests.CategoryRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryReadRepository categoryReadRepository, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _categoryReadRepository = categoryReadRepository;
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult ListAllData()
        {
            var result = _categoryReadRepository.GetAll().ToList();
            return Json(result);
        }
        public async Task<IActionResult> Create()
        {
            var result = await _categoryService.Create();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            var result = await _categoryService.CreateAsync(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.Update(id);
            if (!result.IsSuccess)
            {
                Message(result);
                return RedirectToAction(nameof(List));
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryRequest request)
        {
            var result = await _categoryService.Update(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
