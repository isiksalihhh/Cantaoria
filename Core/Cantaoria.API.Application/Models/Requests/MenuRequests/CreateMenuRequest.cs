namespace Cantaoria.Application.Models.Requests.MenuRequests
{
    public class CreateMenuRequest
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int? ParentID { get; set; }
        public int Order { get; set; }
    }
}
