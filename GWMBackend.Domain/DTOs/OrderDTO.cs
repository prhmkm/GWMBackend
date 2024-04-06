using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class OrderDTO
    {
        public class AddOrder
        {
            public int CustomerId { get; set; }
            public DateTime PickupDate { get; set; }
            public string BucketAmont { get; set; } 
            public int ProductsId { get; set; }
        }

    }
}
