namespace Cantaoria.Application.Models.Requests.MenuRequests
{
    public class UpdateMenuRequest
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? ParentID { get; set; }
        public int Order { get; set; }
    }
}
