using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string? ImageName { get; set; }
        public string? Address { get; set; }
        public string? Thumbnail { get; set; }
    }
}
