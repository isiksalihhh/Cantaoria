using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Application.Models.Requests.CategoryRequests
{
    public class UpdateCategoryRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MenuID { get; set; }
        public List<SelectListItem> Menu { get; set; }
        public bool IsEnabled { get; set; }
    }
}
