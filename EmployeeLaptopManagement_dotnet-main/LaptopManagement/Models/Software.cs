using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class Software
    {
        public Software()
        {
            Laptops = new HashSet<Laptop>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Laptop> Laptops { get; set; }
    }
}
