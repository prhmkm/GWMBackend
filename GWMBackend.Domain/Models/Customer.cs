using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? VerificationCode { get; set; }
        public string RestaurantName { get; set; } = null!;
        public DateTime JoinDate { get; set; }
        public bool RememberMe { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
