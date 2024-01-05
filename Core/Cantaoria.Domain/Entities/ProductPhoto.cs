using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public class ProductPhoto : BaseEntity
    {
        public int ProductID { get; set; }
        public string MainPhoto { get; set; }
        public string? OtherPhoto { get; set; }
        public Product Product { get; set; }
    }
}
