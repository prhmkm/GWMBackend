using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Inventory { get; set; }
    }
}
