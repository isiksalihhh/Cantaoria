namespace Cantaoria.Application.Models.Responses.OrderResponses
{
    public class OrderResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsEnabled { get; set; }

    }
}
