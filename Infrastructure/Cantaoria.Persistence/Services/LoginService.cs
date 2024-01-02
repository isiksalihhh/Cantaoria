using AvvaMobile.Core;
using AvvaMobile.Core.Business;
using AvvaMobile.Core.Extensions;
using AvvaMobile.Core.Utilities.Mail;
using Cantaoria.Application.Models.Requests.LoginRequests;
using Cantaoria.Application.Repositories;
using Cantaoria.Persistence;
using Cantaoria.Persistence.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cantaoria.Persistence.Services
{
    public class LoginService : BaseService, ILoginService
    {
        //private readonly IMailService _mailService;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IRoleReadRepository _roleReadRepository;
        public LoginService(IHttpContextAccessor httpContext,/* IMailService mailService*/ IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IRoleReadRepository roleReadRepository) : base(httpContext)
        {
            //_mailService = mailService;
            _roleReadRepository = roleReadRepository;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<ServiceResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = new ServiceResult();

            var user = _userReadRepository.GetWhere(x => x.Email == request.Email.Trim()).FirstOrDefault();

            if (user is null)
            {
                result.SetError("Kullanıcı bulunamadı");
                return result;
            }

            user.Password = KeyGenerator.CreateRandomPassword(6);
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            var emailParameters = new Dictionary<string, string>
            {
                { "[UserFirstName]", user.FirstName },
                { "[UserLastName]", user.LastName },
                { "[Email]", user.Email },
                { "[Phone]", user.Phone.ToPhoneNumber() },
                { "[Password]", user.Password },
            };

            //var emailResult = await _mailService.Send(
            //    user.Email,
            //    "Yeni Şifreniz Kullanıma Hazır",
            //    "PasswordUpdated",
            //    emailParameters);

            //if (!emailResult.IsSuccess)
            //{
            //    result.SetError(emailResult.Message);
            //    return result;
            //}

            result.SetSuccess("Şifre için email gönderilmiştir.");
            return result;

        }

        public async Task<ServiceResult<UpdateProfileRequest>> GetProfile()
        {
            var result = new ServiceResult<UpdateProfileRequest>();

            var user = await (from u in _userReadRepository.GetAll()
                              join r in _roleReadRepository.GetAll() on u.RoleID equals r.ID
                              where u.ID == CurrentUserID
                              select new UpdateProfileRequest
                              {
                                  ID = u.ID,
                                  FirstName = u.FirstName,
                                  LastName = u.LastName,
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  RoleName = r.Name
                              }).FirstOrDefaultAsync();

            if (user is null)

            {
                result.SetError("Kayıt bulunamadı");
                return result;
            }

            result.Data = user;
            return result;
        }

        public async Task<ServiceResult> SignIn(LoginRequest request)
        {
            var result = new ServiceResult();

            var user = _userReadRepository.GetWhere(x => x.Email == request.Email.Trim() && x.Password == request.Password).FirstOrDefault();

            if (user is null)
            {
                result.SetError("Giriş Başarısız");
                return result;
            }

            var userRole = _roleReadRepository.GetWhere(x => x.ID == user.RoleID).Select(x => x.Name).FirstOrDefault();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Authentication, "true"),
                new Claim(ClaimTypes.Role, userRole)
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "UserAuthentication"));

            await _httpContext.HttpContext.SignInAsync("UserAuthentication", userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMonths(1),
                IsPersistent = true,
                AllowRefresh = true
            });
            return result;
        }

        public async Task<ServiceResult> SignOut()
        {
            var result = new ServiceResult();

            await _httpContext.HttpContext.SignOutAsync();

            result.SetSuccess("Çıkış yapıldı");

            return result;
        }

        public async Task<ServiceResult> UpdateProfile(UpdateProfileRequest request)
        {
            var result = new ServiceResult();

            var user = _userReadRepository.GetWhere(x => x.ID == request.ID).FirstOrDefault();

            if (user is null)
            {
                result.SetError("Kullanıcı bulunamadı");
                return result;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Phone = request.Phone;
            _userWriteRepository.Update(user);
            await _userWriteRepository.SaveAsync();

            result.SetSuccess("Profiliniz güncellenmiştir.");
            return result;
        }
    }
}
