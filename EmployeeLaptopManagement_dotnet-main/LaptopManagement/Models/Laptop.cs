using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class Laptop
    {
        public Laptop()
        {
            InstalledSoftwares = new HashSet<InstalledSoftware>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Storage { get; set; }
        public string? Processor { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; }
    }
}
