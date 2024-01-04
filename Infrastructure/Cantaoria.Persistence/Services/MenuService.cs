using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.MenuRequests;
using Cantaoria.Application.Models.Responses;
using Cantaoria.Application.Models.Responses.UserResponses;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Cantaoria.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Persistence.Services
{
    public class MenuService : BaseService, IMenuService
    {
        private readonly IMenuReadRepository _menuReadRepository;
        private readonly IMenuWriteRepository _menuWriteRepository;
        public MenuService(IHttpContextAccessor httpContext, IMenuWriteRepository menuWriteRepository, IMenuReadRepository menuReadRepository) : base(httpContext)
        {
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
        }

        public async Task<ServiceResult<CreateMenuRequest>> Create()
        {
            var result = new ServiceResult<CreateMenuRequest>();

            var menu = _menuReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            var createMenu = new CreateMenuRequest
            {
                Menus = menu
            };

            result.Data = createMenu;

            return result;
        }

        public async Task<ServiceResult<CreateMenuRequest>> CreateAsync(CreateMenuRequest request)
        {
            var result = new ServiceResult<CreateMenuRequest>();

            var isMenuExist = _menuReadRepository.GetWhere(x => x.Name == request.Name && x.URL == request.URL).Any();

            if (isMenuExist)
            {
                result.SetError("Menu bulunmaktadır.");
                return result;
            }

            var menu = new Menu
            {
                Name = request.Name,
                URL = request.URL,
                Order = request.Order,
                ParentID = string.IsNullOrEmpty(request.ParentID) ? null : int.Parse(request.ParentID),
                IsEnabled = true,
            };

            await _menuWriteRepository.AddAsync(menu);
            await _menuWriteRepository.SaveAsync();

            result.SetSuccess("Menu başarıyla eklendi.");
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var menu = _menuReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            if (menu == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _menuWriteRepository.Delete(menu);
            await _menuWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<List<MenuResponse>>> ListAllData()
        {
            var result = new ServiceResult<List<MenuResponse>>();

            var menus = _menuReadRepository.GetAll();

            var menu = _menuReadRepository.GetAll().Select(c => new MenuResponse
            {
                ID = c.ID,
                Name = c.Name,
                URL = c.URL,
                Order = c.Order,
                IsEnabled = c.IsEnabled,
                ParentMenus = menus
                    .Where(parent => parent.ID == c.ParentID)
                    .Select(parent => parent.Name)
                    .ToList(),
                ChildMenus = menus
                    .Where(child => child.ParentID == c.ID)
                    .Select(child => child.Name)
                    .ToList(),
            }).ToList();

            result.Data = menu;

            return result;
        }

        public async Task<ServiceResult<UpdateMenuRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateMenuRequest>();

            var menu = await _menuReadRepository.GetByIdAsync(id);

            var childMenus = _menuReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            if (menu is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            result.Data = new UpdateMenuRequest
            {
                Name = menu.Name,
                URL = menu.URL,
                Order = menu.Order,
                Menus = childMenus,
                ParentID = string.IsNullOrEmpty(menu.ParentID.ToString()) ? null : menu.ParentID.ToString(),
                IsEnabled = true,
            };

            return result;
        }

        public async Task<ServiceResult<UpdateMenuRequest>> Update(UpdateMenuRequest request)
        {
            var result = new ServiceResult<UpdateMenuRequest>();

            var menu = _menuReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (menu is null)
            {
                result.SetError("Kayıt bulunmamaktadır.");
                return result;
            }

            var isMenuExist = _menuReadRepository.GetWhere(x => x.Name == request.Name && x.URL == request.URL && x.ParentID == int.Parse(request.ParentID)).Any();

            if (isMenuExist)
            {
                result.SetError("Menu bulunmaktadır.");
                return result;
            }

            menu.Order = request.Order;
            menu.URL = request.URL;
            menu.Name = request.Name;
            menu.ParentID = string.IsNullOrEmpty(request.ParentID) ? null : int.Parse(request.ParentID);
            menu.IsEnabled = true;

            _menuWriteRepository.Update(menu);
            await _menuWriteRepository.SaveAsync();

            result.SetSuccess("Menu başarıyla eklendi.");
            return result;
        }
    }
}
