﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Outlook_Work.Models.Entities
{
    public partial class Status
    {
        public Status()
        {
            Order = new HashSet<Order>();
        }

        public int Idstatus { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}