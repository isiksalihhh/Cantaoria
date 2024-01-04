using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cantaoria.Application.Models.Requests.MenuRequests
{
    public class CreateMenuRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string ParentID { get; set; }
        public List<SelectListItem> Menus { get; set; }
        public int Order { get; set; }
        public bool IsEnabled { get; set; }
    }
}
