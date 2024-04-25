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
            public string PickupDate { get; set; }
            public string BucketAmont { get; set; } 
            public List<Products>? Products { get; set; }
        }
        public class Products
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
        public class BOProducts
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Quantity { get; set; }
        }
        public class BOGetAllOrders
        {
            public int Id { get; set; }
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public DateTime PickupDate { get; set; }
            public string BucketAmont { get; set; }
            public bool IsDone { get; set; }
            public List<BOProducts>? Products { get; set; }
            public DateTime RegistrationDate { get; set; }
        }

    }
}
