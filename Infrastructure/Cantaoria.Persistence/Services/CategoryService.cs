using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.CategoryRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Persistence.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        public CategoryService(IHttpContextAccessor httpContext, IMenuReadRepository menuReadRepository, ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository) : base(httpContext)
        {
            _menuReadRepository = menuReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<ServiceResult<CreateCategoryRequest>> Create()
        {
            var result = new ServiceResult<CreateCategoryRequest>();

            var menu = _menuReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            var createCategory = new CreateCategoryRequest
            {
                Menu = menu
            };

            result.Data = createCategory;

            return result;
        }

        public async Task<ServiceResult<CreateCategoryRequest>> CreateAsync(CreateCategoryRequest request)
        {
            var result = new ServiceResult<CreateCategoryRequest>();

            var isCategoryExist = _categoryReadRepository.GetWhere(x => x.Name == request.Name).Any();

            if (isCategoryExist)
            {
                result.SetError("Kategori bulunmaktadır.");
                return result;
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                MenuID = int.Parse(request.MenuID),
                IsEnabled = true,
            };

            await _categoryWriteRepository.AddAsync(category);
            await _categoryWriteRepository.SaveAsync();

            result.SetSuccess("Ürün başarıyla eklendi.");
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var category = _categoryReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            if (category == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _categoryWriteRepository.Delete(category);
            await _categoryWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<UpdateCategoryRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateCategoryRequest>();

            var category = await _categoryReadRepository.GetByIdAsync(id);

            var menu = _menuReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            if (category is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            result.Data = new UpdateCategoryRequest
            {
                Name = category.Name,
                Description = category.Description,
                Menu = menu,
                MenuID = category.MenuID.ToString(),
                IsEnabled = category.IsEnabled,
            };
            return result;
        }

        public async Task<ServiceResult<UpdateCategoryRequest>> Update(UpdateCategoryRequest request)
        {
            var result = new ServiceResult<UpdateCategoryRequest>();

            var category = _categoryReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (category == null)
            {
                result.SetError("Kayıt bulunmamaktadır.");
                return result;
            }

            var isCategoryExist = _categoryReadRepository.GetWhere(x => x.Name == request.Name && x.Description == request.Description).Any();

            if (isCategoryExist)
            {
                result.SetError("Kategori bulunmaktadır.");
                return result;
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.MenuID = int.Parse(request.MenuID);
            category.IsEnabled = true;

            _categoryWriteRepository.Update(category);
            await _categoryWriteRepository.SaveAsync();

            result.SetSuccess("Kategori başarıyla eklendi.");
            return result;
        }
    }
}
