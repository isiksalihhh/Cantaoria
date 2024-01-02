using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string UserID { get; set; }
        public string RoleID { get; set; }
    }
}
