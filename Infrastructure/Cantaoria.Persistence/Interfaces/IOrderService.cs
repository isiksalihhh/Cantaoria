using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Responses.OrderResponses;

namespace Cantaoria.Persistence.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult<List<OrderResponse>>> ListAllData();
    }
}
