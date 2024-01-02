using Cantaoria.Domain.Entities;

namespace Cantaoria.Application.Models.Requests.OrderRequests
{
    public class CreateOrderRequest
    {
        public string CustomerID { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
    }
}
