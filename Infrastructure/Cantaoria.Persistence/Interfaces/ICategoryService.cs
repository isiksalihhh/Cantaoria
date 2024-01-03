using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.CategoryRequests;

namespace Cantaoria.Persistence.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResult<CreateCategoryRequest>> Create();
        Task<ServiceResult<CreateCategoryRequest>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult<UpdateCategoryRequest>> Update(int id);
        Task<ServiceResult<UpdateCategoryRequest>> Update(UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
