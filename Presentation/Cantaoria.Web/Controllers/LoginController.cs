using AvvaMobile.Core;
using Cantaoria.Application.Interfaces;
using Cantaoria.Application.Models.Requests.LoginRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            loginService = _loginService;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View("SignIn2");
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginRequest request)
        {
            var result = await _loginService.SignIn(request);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            Message(result);
            return View("SignIn2");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword2");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _loginService.ForgotPassword(request);
            Message(result);
            return View("ForgotPassword2");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Sign_Out()
        {
            await _loginService.SignOut();
            return RedirectToAction(nameof(SignIn));
        }

        public async Task<IActionResult> MyProfile()
        {
            var result = await _loginService.GetProfile();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        {
            var result = await _loginService.UpdateProfile(request);
            Message(result);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
