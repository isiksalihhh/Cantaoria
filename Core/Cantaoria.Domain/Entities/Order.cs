using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public  class Order : BaseEntity
    {
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<Product> Products { get; set; }
        public Customer Customer { get; set; }
    }
}
