using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Password { get; set; } = null!;
        public sbyte? Role { get; set; }
        public int? LaptopId { get; set; }

        public virtual Laptop? Laptop { get; set; }
    }
}
