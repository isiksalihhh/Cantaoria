using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
