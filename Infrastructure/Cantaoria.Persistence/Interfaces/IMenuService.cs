using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.MenuRequests;
using Cantaoria.Application.Models.Responses;

namespace Cantaoria.Persistence.Interfaces
{
    public interface IMenuService
    {
        Task<ServiceResult<List<MenuResponse>>> ListAllData();
        Task<ServiceResult<CreateMenuRequest>> Create();
        Task<ServiceResult<CreateMenuRequest>> CreateAsync(CreateMenuRequest request);
        Task<ServiceResult<UpdateMenuRequest>> Update(int id);
        Task<ServiceResult<UpdateMenuRequest>> Update(UpdateMenuRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
