using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Responses.CustomerResponses;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cantaoria.Persistence.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IUserReadRepository _userReadRepository;
        public CustomerService(IHttpContextAccessor httpContext, IUserReadRepository userReadRepository) : base(httpContext)
        {
            _userReadRepository = userReadRepository;
        }

        public async Task<ServiceResult<List<CustomerResponse>>> ListAllData()
        {
            var result = new ServiceResult<List<CustomerResponse>>();

            var customers = _userReadRepository.GetWhere(x=>x.RoleID == 4).Select(x => new CustomerResponse
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                IsEnabled = x.IsEnabled,
            }).ToList();

            result.Data = customers;

            return result;
        }
    }
}
