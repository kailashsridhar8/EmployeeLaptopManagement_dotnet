﻿using System;
using System.Collections.Generic;

namespace LaptopManagement.Models
{
    public partial class InstalledSoftware
    {
        public int Id { get; set; }
        public int? LaptopId { get; set; }
        public int? SoftwareId { get; set; }
    }
}
