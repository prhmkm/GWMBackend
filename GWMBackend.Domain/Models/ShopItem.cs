using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class ShopItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amont { get; set; }
    }
}
