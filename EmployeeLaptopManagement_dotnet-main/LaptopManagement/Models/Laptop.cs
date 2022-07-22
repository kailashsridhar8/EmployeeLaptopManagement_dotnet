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
        public int SoftwareId { get; set; }

        public virtual Software Software { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
