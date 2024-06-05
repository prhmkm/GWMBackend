using Microsoft.AspNetCore.Http;
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
            public string Price { get; set; }
            public string? Description { get; set; }
            public string? Photo { get; set; }
        }
        public class BOGetAllProducts
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Inventory { get; set; }
            public string Price { get; set; }
            public string? Description { get; set; }
            public string? Photo { get; set; }
            public bool IsActive { get; set; }
        }
        public class AddProduct
        {
            public string Title { get; set; }
            public int Inventory { get; set; }
            public string Price { get; set; }
            public string? Description { get; set; }
            public string? Photo { get; set; }
            public string? PhotoName { get; set; }

        }
        public class EditProduct
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public int? Inventory { get; set; }
            public string? Price { get; set; }
            public string? Description { get; set; }
            public string? Photo { get; set; }
            public string? PhotoName { get; set; }
            public bool? IsActive { get; set; }
        }
    }
}
