using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class Laptop
    {
        public Laptop()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Storage { get; set; }
        public string? Processor { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
