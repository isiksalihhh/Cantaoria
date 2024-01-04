using AvvaMobile.Core;
using Cantaoria.Application.Models.Requests.MenuRequests;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult List()
        {
            return View();
        }

        public async Task<IActionResult> ListAllData()
        {
            var result = await _menuService.ListAllData();
            return Json(result.Data);
        }
        public async Task<IActionResult> Create()
        {
            var result = await _menuService.Create();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuRequest request)
        {
            var result = await _menuService.CreateAsync(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _menuService.Update(id);
            if (!result.IsSuccess)
            {
                Message(result);
                return RedirectToAction(nameof(List));
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMenuRequest request)
        {
            var result = await _menuService.Update(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _menuService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
