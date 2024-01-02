﻿using Cantaoria.Domain.Entities.Common;

namespace Cantaoria.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int? ParentID { get; set; }
        public int Order{ get; set; }
        public List<Category> Category { get; set; }
    }
}