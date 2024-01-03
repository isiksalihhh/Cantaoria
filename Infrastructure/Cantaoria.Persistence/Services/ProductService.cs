using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.ProductRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Persistence.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        public ProductService(IHttpContextAccessor httpContext, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository ) : base(httpContext)
        {
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public ServiceResult<CreateProductRequest> Create()
        {
            var result = new ServiceResult<CreateProductRequest>();

            var category = _categoryReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            var createProduct = new CreateProductRequest
            {
                Category = category
            };

            result.Data = createProduct;

            return result;
        }

        public async Task<ServiceResult<CreateProductRequest>> CreateAsync(CreateProductRequest request)
        {
            var result = new ServiceResult<CreateProductRequest>();

            var isProductExist = _productReadRepository.GetWhere(x => x.Name == request.Name).Any();

            if (isProductExist)
            {
                result.SetError("Ürün bulunmaktadır.");
                return result;
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryID = int.Parse(request.CategoryID),
                IsEnabled = true,
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync();

            result.SetSuccess("Ürün başarıyla eklendi.");
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var page =  _productReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            if (page == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _productWriteRepository.Delete(page);
            await _productWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<UpdateProductRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateProductRequest>();

            var product = await _productReadRepository.GetByIdAsync(id);

            var category = _categoryReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            if (product is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            result.Data = new UpdateProductRequest
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = category,
                CategoryID = product.CategoryID.ToString(),
                IsEnabled = product.IsEnabled,
            };

            return result;
        }

        public async Task<ServiceResult<UpdateProductRequest>> Update(UpdateProductRequest request)
        {
            var result = new ServiceResult<UpdateProductRequest>();

            var product = _productReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (product == null)
            {
                result.SetError("Kayıt bulunmamaktadır.");
                return result;
            }

            var isProductExist = _productReadRepository.GetWhere(x => x.Name == request.Name && x.Description == request.Description).Any();

            if (isProductExist)
            {
                result.SetError("Ürün bulunmaktadır.");
                return result;
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.CategoryID = int.Parse(request.CategoryID);
            product.IsEnabled = true;

             _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();

            result.SetSuccess("Ürün başarıyla eklendi.");
            return result;
        }
    }
}
