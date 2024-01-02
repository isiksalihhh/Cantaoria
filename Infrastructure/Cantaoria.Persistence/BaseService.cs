using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Cantaoria.Persistence
{
    public class BaseService
    {
        protected IHttpContextAccessor _httpContext;
        public BaseService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public bool CurrentUserAuthenticated
        {
            get
            {
                return bool.Parse(_httpContext.HttpContext.User.Identities
                    .FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.Authentication))
                    ?.FindFirst(ClaimTypes.Authentication).Value);
            }
        }
        public int CurrentUserID
        {
            get
            {
                return int.Parse(_httpContext.HttpContext.User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))?.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
        }
        public string CurrentUserAnonymousID
        {
            get
            {
                return _httpContext.HttpContext.User.Identities
                    .FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                    ?.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
        public string CurrentUserName
        {
            get
            {
                return _httpContext.HttpContext.User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.Name))?.FindFirst(ClaimTypes.Name).Value;
            }
        }
        public string CurrentUserEmail
        {
            get
            {
                return _httpContext.HttpContext.User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.Email))?.FindFirst(ClaimTypes.Email).Value;
            }
        }

        public string CurrentUserPhone
        {
            get
            {
                return _httpContext.HttpContext.User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.MobilePhone))?.FindFirst(ClaimTypes.MobilePhone).Value;
            }
        }

        public static string GetMethodName([CallerMemberName] string name = "") => name;
    }
}
