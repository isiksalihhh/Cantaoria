using Cantaoria.Domain.Entities;

namespace Cantaoria.Application.Models.Responses
{
    public class MenuResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public List<string> ParentMenus { get; set; }
        public List<string> ChildMenus { get; set; }
        public int Order { get; set; }
        public bool IsEnabled { get; set; }
    }
}
