using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cantaoria.Web.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public OrderController(IOrderWriteRepository orderWriteRepository, IOrderService orderService)
        {
            _orderService = orderService;
            _orderWriteRepository = orderWriteRepository;
        }

        public IActionResult List()
        {
            return View();
        }
        public async Task<IActionResult> ListAllData()
        {
            var result = await _orderService.ListAllData();

            return Json(result.Data);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _orderWriteRepository.DeleteAsync(id);
            await _orderWriteRepository.SaveAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
