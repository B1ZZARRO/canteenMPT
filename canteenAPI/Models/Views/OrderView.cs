using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canteenAPI.Models.Views
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public float Cost { get; set; }
        public string PayStatus { get; set; }
        public string OrderStatus { get; set; }
        public List<AvaibilityView> Avaibility { get; set; }
    }
}
