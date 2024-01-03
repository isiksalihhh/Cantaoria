using Cantaoria.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;

        public CustomerController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public IActionResult List()
        {
            return View();
        }
        public IActionResult ListAllData()
        {
            var result = _customerReadRepository.GetAll().ToList();
            return Json(result);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _customerWriteRepository.DeleteAsync(id);
            await _customerWriteRepository.SaveAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
