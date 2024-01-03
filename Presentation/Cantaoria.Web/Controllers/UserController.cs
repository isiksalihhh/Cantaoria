using AvvaMobile.Core;
using Cantaoria.Application.Models.Requests.UserRequests;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult List()
        {
            return View();
        }

        public async Task<IActionResult> ListAllData()
        {
            var result = await _userService.ListAllData();
            return Json(result.Data);
        }
        public async Task<IActionResult> Create()
        {
            var result = await _userService.Create();

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var result = await _userService.CreateAsync(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _userService.Update(id);
            if (!result.IsSuccess)
            {
                Message(result);
                return RedirectToAction(nameof(List));
            }
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            var result = await _userService.Update(request);
            Message(result);
            if (!result.IsSuccess)
            {
                return View(result.Data);
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }
    }
}
