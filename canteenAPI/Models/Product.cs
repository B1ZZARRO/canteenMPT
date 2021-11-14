using System;
using System.Collections.Generic;

#nullable disable

namespace canteenAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Avaibilities = new HashSet<Avaibility>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Cost { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Avaibility> Avaibilities { get; set; }
    }
}
