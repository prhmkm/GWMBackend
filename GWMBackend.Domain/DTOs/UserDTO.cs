using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class UserDTO
    {
        public class UserResponse
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public string RoleTitle { get; set; }
            public string UserName { get; set; }
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? Image { get; set; }
            public string MobileNumber { get; set; }
        }
        public class AddUserRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string PassWord { get; set; }
            public int RoleId { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string? Image { get; set; }
        }
        public class EditUserRequest
        {
            public int UserId { get; set; }
            public int RoleId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public bool IsActive { get; set; }
        }
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
        public class LoginResponse
        {
            public int Id { get; set; }
            public int RoleId { get; set; }
            public string RoleTitle { get; set; }
            public string UserName { get; set; }
            public string DisplayName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public string Image { get; set; }
            public DateTime CreationDateTime { get; set; }
        }
        public class RefreshTokenRequest
        {
            public string Username { get; set; }
            public string RefreshToken { get; set; }
        }
        public class ChangePass
        {
            public int Id { get; set; }
            public string oldPassword { get; set; }
            public string newPassword { get; set; }
        }
    }
}
