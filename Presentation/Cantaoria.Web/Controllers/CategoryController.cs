using Cantaoria.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        public CategoryController(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
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
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryWriteRepository.DeleteAsync(id);
            await _categoryWriteRepository.SaveAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
