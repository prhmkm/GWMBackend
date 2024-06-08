using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
