using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public sbyte? Role { get; set; }
        public int? LaptopId { get; set; }

        public virtual Laptop? Laptop { get; set; }
    }
}
