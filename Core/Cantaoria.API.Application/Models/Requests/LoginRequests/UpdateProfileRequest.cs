namespace Cantaoria.Application.Models.Requests.LoginRequests
{
    public class UpdateProfileRequest
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
    }
}
