using System;
using System.Collections.Generic;

namespace GWMBackend.Domain.Models
{
    public partial class BucketAmount
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }
}
