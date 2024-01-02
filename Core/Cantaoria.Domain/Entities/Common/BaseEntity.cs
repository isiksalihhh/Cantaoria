namespace Cantaoria.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
