using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Responses.OrderResponses;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cantaoria.Persistence.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        public OrderService(IHttpContextAccessor httpContext, ICustomerReadRepository customerReadRepository, IOrderReadRepository orderReadRepository) : base(httpContext)
        {
            _orderReadRepository = orderReadRepository;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<ServiceResult<List<OrderResponse>>> ListAllData()
        {
            var result = new ServiceResult<List<OrderResponse>>();

            var customer = _customerReadRepository.GetAll();

            var orders = _orderReadRepository.GetAll().Select(x=> new OrderResponse
            {
                ID = x.ID,
                Address = x.Address,
                Description = x.Description,
                OrderDate = x.OrderDate,
                TotalAmount = x.TotalAmount,
                IsEnabled = x.IsEnabled,
                Name = customer.Where(c => c.ID == x.CustomerID).Select(c => $"{c.FirstName} {c.LastName}").FirstOrDefault()

            }).ToList();

            result.Data = orders;

            return result;
        }
    }
}
