namespace Cantaoria.Application.Models.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
