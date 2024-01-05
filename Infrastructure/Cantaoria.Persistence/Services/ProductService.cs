using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.ProductRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Persistence.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductPhotoReadRepository _productPhotoReadRepository;
        private readonly IProductPhotoWriteRepository _productPhotoWriteRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductService(IHttpContextAccessor httpContext, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ICategoryReadRepository categoryReadRepository, IProductPhotoReadRepository productPhotoReadRepository, IProductPhotoWriteRepository productPhotoWriteRepository, IHostingEnvironment hostingEnvironment) : base(httpContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _productPhotoWriteRepository = productPhotoWriteRepository;
            _productPhotoReadRepository = productPhotoReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<ServiceResult<CreateProductRequest>> Create()
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

            var isProductExist = _productReadRepository.GetWhere(x => x.Name == request.Name && x.CategoryID == int.Parse(request.CategoryID)).Any();

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

            var categoryName = _categoryReadRepository.GetWhere(x => x.ID == product.CategoryID).Select(x=>x.Name).FirstOrDefault();

            var tempFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Temp");

            var mainPhotoFileName = (request.MainPhoto != null) ? $"Main_{Guid.NewGuid()}_{categoryName}_{product.Name}_{request.MainPhoto.FileName}" : null;

            for (int i = 0; i < request.OtherPhotos.Count; i++)
            {
                var productPhoto = new ProductPhoto
                {
                    ProductID = product.ID,
                    Product = product,
                    MainPhoto = $"{tempFolderPath}\\{mainPhotoFileName}",

                    IsEnabled = true,
                };
                await _productPhotoWriteRepository.AddAsync(productPhoto);
            }

            await _productPhotoWriteRepository.SaveAsync();

            CopyFileToTemp(request.MainPhoto, mainPhotoFileName);
          
                if (request.OtherPhotos != null && request.OtherPhotos.Any())
                {

                var productPhotos = _productPhotoReadRepository.GetWhere(x => x.ProductID == product.ID).ToList();

                for (int i = 0; i < request.OtherPhotos.Count; i++)
                {
                    var x = request.OtherPhotos[i];

                    var productPhoto = productPhotos[i];

                    var otherPhotoFileName = $"Other_{Guid.NewGuid()}_{categoryName}_{product.Name}_{x.FileName}";

                    productPhoto.OtherPhoto = $"{tempFolderPath}\\{otherPhotoFileName}";

                    _productPhotoWriteRepository.Update(productPhoto);
                }
            }

            await _productPhotoWriteRepository.SaveAsync();

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var product =  _productReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            var productPhoto = _productPhotoReadRepository.GetWhere(x => x.ProductID == id).FirstOrDefault();

            if (product == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _productPhotoWriteRepository.Delete(productPhoto);
            _productWriteRepository.Delete(product);
            await _productWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<UpdateProductRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateProductRequest>();

            var product = await _productReadRepository.GetByIdAsync(id);

            var category = _categoryReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            var productPhoto = _productPhotoReadRepository.GetWhere(x => x.ProductID == id);

            if (product is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            var updateProductRequest = new UpdateProductRequest
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Category = category,
                CategoryID = product.CategoryID.ToString(),
                IsEnabled = product.IsEnabled,
                MainPhotoURL = productPhoto.FirstOrDefault().MainPhoto,
                OtherPhotosURL = productPhoto.Select(x => x.OtherPhoto).ToList(),
                OtherPhotosCount = productPhoto.Count(),
            };

            result.Data = updateProductRequest;

            return result;
        }

        public async Task<ServiceResult<UpdateProductRequest>> Update(UpdateProductRequest request)
        {
            var result = new ServiceResult<UpdateProductRequest>();

            var product = _productReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            var productPhotos = _productPhotoReadRepository.GetWhere(x => x.ProductID == request.ID).ToList();

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

            var categoryName = _categoryReadRepository.GetWhere(x => x.ID == product.CategoryID).Select(x => x.Name).FirstOrDefault();

            var tempFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Temp");

            var mainPhotoFileName = (request.MainPhoto != null) ? $"Main_{Guid.NewGuid()}_{categoryName}_{product.Name}_{request.MainPhoto.FileName}" : null;

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.CategoryID = int.Parse(request.CategoryID);
            product.IsEnabled = true;

            for (int i = 0; i < request.OtherPhotos.Count; i++)
            {
                var x = request.OtherPhotos[i];

                var productPhoto = productPhotos[i];

                var otherPhotoFileName = $"Other_{Guid.NewGuid()}_{categoryName}_{product.Name}_{x.FileName}";

                productPhoto.MainPhoto = $"{tempFolderPath}\\{mainPhotoFileName}";
                productPhoto.OtherPhoto = $"{tempFolderPath}\\{otherPhotoFileName}";


                _productPhotoWriteRepository.Update(productPhoto);
            }

             _productWriteRepository.Update(product);
            await _productPhotoWriteRepository.SaveAsync();
            await _productWriteRepository.SaveAsync();

            result.SetSuccess("Ürün başarıyla eklendi.");
            return result;
        }

        private void CopyFileToTemp(IFormFile file, string newFileName)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string tempFolderPath = Path.Combine(webRootPath, "temp");

            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }

            string filePath = Path.Combine(tempFolderPath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }
    }
}
