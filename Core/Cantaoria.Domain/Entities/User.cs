﻿using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set;}
    }
}
