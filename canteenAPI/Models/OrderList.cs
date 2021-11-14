using System;
using System.Collections.Generic;

#nullable disable

namespace canteenAPI.Models
{
    public partial class OrderList
    {
        public int OrderListId { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public int AvaibilityId { get; set; }

        public virtual Avaibility Avaibility { get; set; }
        public virtual Order Order { get; set; }
    }
}
