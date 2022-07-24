using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class User
    {
        public User()
        {
            Laptops = new HashSet<Laptop>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public sbyte? Role { get; set; }

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
