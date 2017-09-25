using System;
using System.Collections.Generic;
using System.Text;

namespace ShopHierarchy
{
    public class Item
    {
        public Item()
        {
            
        }
        public Item(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
            this.Orders = new HashSet<OrdersItems>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<OrdersItems> Orders { get; set; }
    }
}