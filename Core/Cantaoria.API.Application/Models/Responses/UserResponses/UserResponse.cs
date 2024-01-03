namespace Cantaoria.Application.Models.Responses.UserResponses
{
    public class UserResponse
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
