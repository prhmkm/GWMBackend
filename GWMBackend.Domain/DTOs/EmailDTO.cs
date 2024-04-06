using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class EmailDTO
    {
        public class VerifyEmail
        {
            public string Email { get; set; }
        }
        public class VerifyCode
        {
            public int Id { get; set; }
            public string Code { get; set; }
        }
        public class CLoginResponse
        {

            public string DisplayName { get; set; }
            public string Mobile { get; set; }
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public DateTime CreationDateTime { get; set; }
        }
    }
}
