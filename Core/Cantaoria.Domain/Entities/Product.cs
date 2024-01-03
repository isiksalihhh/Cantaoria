using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        //public ICollection<Order> Orders { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
