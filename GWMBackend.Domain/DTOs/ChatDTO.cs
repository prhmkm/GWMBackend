using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWMBackend.Domain.DTOs
{
    public class ChatDTO
    {
        public class UserConnection
        {
            public string UserName { get; set; }
            public string ChatRoom { get; set; }
        }
    }
}
