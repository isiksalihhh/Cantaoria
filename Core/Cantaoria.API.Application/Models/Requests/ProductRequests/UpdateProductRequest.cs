using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Application.Models.Requests.ProductRequests
{
    public class UpdateProductRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public string CategoryID { get; set; }
        public IFormFile MainPhoto { get; set; }
        public string MainPhotoURL { get; set; }
        public int OtherPhotosCount { get; set; }
        public List<IFormFile> OtherPhotos { get; set; }
        public List<string> OtherPhotosURL { get; set; }
        public bool IsEnabled { get; set; }
        public List<SelectListItem> Category { get; set; }
    }
}
