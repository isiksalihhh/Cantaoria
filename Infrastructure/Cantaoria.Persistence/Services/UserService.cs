using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.RoleRequests;
using Cantaoria.Application.Models.Requests.UserRequests;
using Cantaoria.Application.Models.Responses.UserResponses;
using Cantaoria.Application.Repositories;
using Cantaoria.Domain.Entities;
using Cantaoria.Persistence.Interfaces;
using Cantaoria.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Persistence.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IRoleReadRepository _roleReadRepository;
        public UserService(IHttpContextAccessor httpContext, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IRoleReadRepository roleReadRepository) : base(httpContext)
        {
            _roleReadRepository = roleReadRepository;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<ServiceResult<List<UserResponse>>> ListAllData()
            {
            var result = new ServiceResult<List<UserResponse>>();

            var role = _roleReadRepository.GetAll();

            var user = _userReadRepository.GetAll().Select(c => new UserResponse
            {
                ID = c.ID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                IsEnabled = c.IsEnabled,
                RoleName = role.Where(x => x.ID == c.RoleID).Select(c => c.Name).FirstOrDefault()
            }).ToList();

            result.Data = user;

            return result;
        }
        public async Task<ServiceResult<CreateUserRequest>> Create()
        {
            var result = new ServiceResult<CreateUserRequest>();

            var role = _roleReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            var createUser = new CreateUserRequest
            {
                Role = role
            };

            result.Data = createUser;

            return result;
        }

        public async Task<ServiceResult<CreateUserRequest>> CreateAsync(CreateUserRequest request)
        {
            var result = new ServiceResult<CreateUserRequest>();

            var isUserExist = _userReadRepository.GetWhere(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.Email == request.Email).Any();

            if (isUserExist)
            {
                result.SetError("Kullanıcı bulunmaktadır.");
                return result;
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Phone = request.Phone,
                RoleID = int.Parse(request.RoleID),
                IsEnabled = true,
            };

            await _userWriteRepository.AddAsync(user);
            await _userWriteRepository.SaveAsync();

            result.SetSuccess("Kullanıcı başarıyla eklendi.");
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            var user = _userReadRepository.GetWhere(x => x.ID == id).FirstOrDefault();

            if (user == null)
            {
                result.SetError("Kayıt bulunamadı.");
                return result;
            }
            _userWriteRepository.Delete(user);
            await _userWriteRepository.SaveAsync();
            result.SetSuccess("Kayıt başarıyla silindi");
            return result;
        }

        public async Task<ServiceResult<UpdateUserRequest>> Update(int id)
        {
            var result = new ServiceResult<UpdateUserRequest>();

            var user = await _userReadRepository.GetByIdAsync(id);

            var role = _roleReadRepository.GetAll().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            if (user is null)
            {
                result.SetError("Aradığınız kayıt bulunamadı.");
                return result;
            }

            result.Data = new UpdateUserRequest
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = role,
                RoleID = user.RoleID.ToString(),
                Phone = user.Phone,
            };

            return result;
        }

        public async Task<ServiceResult<UpdateUserRequest>> Update(UpdateUserRequest request)
        {
            var result = new ServiceResult<UpdateUserRequest>();

            var user = _userReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (user is null)
            {
                result.SetError("Kayıt bulunmamaktadır.");
                return result;
            }

            var isUserExist = _userReadRepository.GetWhere(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.Email == request.Email && x.Password == request.Password && x.RoleID == int.Parse(request.RoleID)).Any();

            if (isUserExist)
            {
                result.SetError("Kullanıcı bulunmaktadır.");
                return result;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;
            user.RoleID = int.Parse(request.RoleID);
            user.Phone = request.Phone;
            user.IsEnabled = true;
            
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            result.SetSuccess("Kullanıcı başarıyla eklendi.");
            return result;
        }
    }
}
