using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.RoleRequests;

namespace Cantaoria.Persistence.Interfaces
{
    public interface IRoleService
    {
        ServiceResult<CreateRoleRequest> Create();
        Task<ServiceResult<CreateRoleRequest>> CreateAsync(CreateRoleRequest request);
        Task<ServiceResult<UpdateRoleRequest>> Update(int id);
        Task<ServiceResult<UpdateRoleRequest>> Update(UpdateRoleRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
