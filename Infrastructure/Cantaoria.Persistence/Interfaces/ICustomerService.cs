using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Responses.CustomerResponses;

namespace Cantaoria.Persistence.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<List<CustomerResponse>>> ListAllData();
    }
}
