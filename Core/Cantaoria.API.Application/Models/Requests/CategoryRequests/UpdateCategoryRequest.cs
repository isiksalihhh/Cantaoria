namespace Cantaoria.Application.Models.Requests.CategoryRequests
{
    public class UpdateCategoryRequest
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
