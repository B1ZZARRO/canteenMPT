using System;
using System.Collections.Generic;

#nullable disable

namespace canteenAPI.Models
{
    public partial class Building
    {
        public Building()
        {
            Avaibilities = new HashSet<Avaibility>();
        }

        public int BuildingId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Avaibility> Avaibilities { get; set; }
    }
}
