using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class ChatLog
    {
        public int Id { get; set; }
        public string SenderUsername { get; set; } = null!;
        public string ChatRoom { get; set; } = null!;
        public string MessageContent { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
    }
}
