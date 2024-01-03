using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {

            _customerService = customerService;
        }

        public IActionResult List()
        {
            return View();
        }
        public async Task<IActionResult> ListAllData()
        {
            var result = await _customerService.ListAllData();

            return Json(result.Data);
        }
    }
}
