using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.ProductRequests;

namespace Cantaoria.Persistence.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<CreateProductRequest>> Create();
        Task<ServiceResult<CreateProductRequest>> CreateAsync(CreateProductRequest request);
        Task<ServiceResult<UpdateProductRequest>> Update(int id);
        Task<ServiceResult<UpdateProductRequest>> Update(UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
