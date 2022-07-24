using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class Software
    {
        public Software()
        {
            InstalledSoftwares = new HashSet<InstalledSoftware>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<InstalledSoftware> InstalledSoftwares { get; set; }
    }
}
