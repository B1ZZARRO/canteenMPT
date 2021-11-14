using System;
using System.Collections.Generic;

#nullable disable

namespace canteenAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public float Cost { get; set; }
        public string PayStatus { get; set; }
        public string OrderStatus { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
