using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.UserRequests;
using Cantaoria.Application.Models.Responses.UserResponses;

namespace Cantaoria.Persistence.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<List<UserResponse>>> ListAllData();
        Task<ServiceResult<CreateUserRequest>> Create();
        Task<ServiceResult<CreateUserRequest>> CreateAsync(CreateUserRequest request);
        Task<ServiceResult<UpdateUserRequest>> Update(int id);
        Task<ServiceResult<UpdateUserRequest>> Update(UpdateUserRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
