using AvvaMobile.Core;
using Cantaoria.Application.Models.Requests.RoleRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleReadRepository _roleReadRepository;
        private readonly IRoleService _roleService;
        public RoleController(IRoleReadRepository roleReadRepository, IRoleService roleService)
        {
            _roleReadRepository = roleReadRepository;
            _roleService = roleService;
        }
        public IActionResult List()
        {
            return View();
        }

        public IActionResult ListAllData()
        {
            var result = _roleReadRepository.GetAll().ToList();
            return Json(result);
        }
        public IActionResult Create()
        {
            var result = _roleService.Create();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequest request)
        {
            var result = await _roleService.CreateAsync(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _roleService.Update(id);
            if (!result.IsSuccess)
            {
                Message(result);
                return RedirectToAction(nameof(List));
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleRequest request)
        {
            var result = await _roleService.Update(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
