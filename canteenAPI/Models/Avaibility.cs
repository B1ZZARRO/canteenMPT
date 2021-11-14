using System;
using System.Collections.Generic;

#nullable disable

namespace canteenAPI.Models
{
    public partial class Avaibility
    {
        public Avaibility()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int AvaibilityId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
