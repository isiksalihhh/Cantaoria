using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Application.Models.Requests.ProductRequests
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public string CategoryID { get; set; }
        public bool IsEnabled { get; set; }
        public List<SelectListItem> Category { get; set; }
    }
}
