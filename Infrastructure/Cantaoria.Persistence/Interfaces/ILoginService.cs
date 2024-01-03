using AvvaMobile.Core.Business;
using Cantaoria.Application.Models.Requests.LoginRequests;

namespace Cantaoria.Persistence.Interfaces
{
    public interface ILoginService
    {
        Task<ServiceResult> SignIn(LoginRequest request);
        Task<ServiceResult> SignOut();
        Task<ServiceResult<UpdateProfileRequest>> GetProfile();
        Task<ServiceResult> UpdateProfile(UpdateProfileRequest request);
        Task<ServiceResult> ForgotPassword(ForgotPasswordRequest request);
        Task<ServiceResult> Register(RegisterRequest request);
    }
}
