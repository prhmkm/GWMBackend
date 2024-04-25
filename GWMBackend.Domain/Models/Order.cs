using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime PickupDate { get; set; }
        public string BucketAmont { get; set; } = null!;
        public bool IsDone { get; set; }
        public DateTime CreationDatetime { get; set; }
    }
}
