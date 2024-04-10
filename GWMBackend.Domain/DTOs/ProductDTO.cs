using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class ProductDTO
    {
        public class GetAllProducts
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Inventory { get; set; }
        }
    }
}
