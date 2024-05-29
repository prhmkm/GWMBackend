using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RestaurantName { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? VerificationCode { get; set; }
        public DateTime JoinDate { get; set; }
        public bool RememberMe { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsRegister { get; set; }
        public bool? IsActive { get; set; }
    }
}
