using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? VerificationCode { get; set; }
        public DateTime JoinDate { get; set; }
        public bool RememberMe { get; set; }
        public string? RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
