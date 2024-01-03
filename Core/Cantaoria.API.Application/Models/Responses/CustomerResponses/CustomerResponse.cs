namespace Cantaoria.Application.Models.Responses.CustomerResponses
{
    public class CustomerResponse
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEnabled{ get; set; }
    }
}
