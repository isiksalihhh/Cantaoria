using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.ProductRequests;
using Cantaoria.Application.Models.Requests.RoleRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Cantaoria.Persistence.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IRoleReadRepository _roleReadRepository;
        private readonly IRoleWriteRepository _roleWriteRepository;
        public RoleService(IHttpContextAccessor httpContext, IRoleWriteRepository roleWriteRepository, IRoleReadRepository roleReadRepository) : base(httpContext)
        {
            _roleReadRepository = roleReadRepository;
            _roleWriteRepository = roleWriteRepository;
        }

        public ServiceResult<CreateRoleRequest> Create()
        {
            var result = new ServiceResult<CreateRoleRequest>();
            var createRole = new CreateRoleRequest();

            result.Data = createRole;

            return result;
        }

        public async Task<ServiceResult<CreateRoleRequest>> CreateAsync(CreateRoleRequest request)
        {
            var result = new ServiceResult<CreateRoleRequest>();

            var isRoleExist = _roleReadRepository.GetWhere(x => x.Name == request.Name).Any();

            if (isRoleExist)
            {
                result.SetError("Ürün bulunmaktadır.");
                return result;
            }

            var role = new Role
            {
                Name = request.Name,
                Description = request.Description,
                IsEnabled = true,
            };

            await _roleWriteRepository.AddAsync(role);
            await _roleWriteRepository.SaveAsync();

            result.SetSuccess("Rol başarıyla eklendi.");
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var role = _roleReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            if (role == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _roleWriteRepository.Delete(role);
            await _roleWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<UpdateRoleRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateRoleRequest>();

            var role = await _roleReadRepository.GetByIdAsync(id);
            
            if (role is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            result.Data = new UpdateRoleRequest
            {
                Name = role.Name,
                Description = role.Description,
            };

            return result;
        }

        public async Task<ServiceResult<UpdateRoleRequest>> Update(UpdateRoleRequest request)
        {
            var result = new ServiceResult<UpdateRoleRequest>();

            var role = _roleReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (role is null)
            {
                result.SetError("Kayıt bulunmamaktadır.");
                return result;
            }

            var isRoleExist = _roleReadRepository.GetWhere(x => x.Name == request.Name && x.Description == request.Description).Any();

            if (isRoleExist)
            {
                result.SetError("Rol bulunmaktadır.");
                return result;
            }

            role.Name = request.Name;
            role.Description = request.Description;
            role.IsEnabled = true;

            _roleWriteRepository.Update(role);
            await _roleWriteRepository.SaveAsync();

            result.SetSuccess("Rol başarıyla eklendi.");
            return result;
        }
    }
}
