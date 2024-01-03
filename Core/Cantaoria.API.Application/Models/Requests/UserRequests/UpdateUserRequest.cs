using Cantaoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Application.Models.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleID { get; set; }
        public List<SelectListItem> Role{ get; set; }
    }
}
