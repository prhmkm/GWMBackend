using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class CustomerDTO
    {
        public class BOGetAllCustomers
        {
            public int Id { get; set; }
            public int RoleId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string RestaurantName { get; set; }
            public string ZipCode { get; set; }
            public string Address { get; set; }
            public DateTime JoinDate { get; set; }
            public bool IsRegister { get; set; }
            public bool? IsActive { get; set; }
        }
        public class AddCustomer
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string RestaurantName { get; set; }
            public string ZipCode { get; set; }
            public string Address { get; set; }
        }
    }
}
