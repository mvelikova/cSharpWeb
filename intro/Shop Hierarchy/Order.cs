using System.Collections.Generic;

namespace ShopHierarchy
{
    public class Order
    {
        public Order()
        {
            this.Items = new HashSet<OrdersItems>();
        }

        public int Id { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public ICollection<OrdersItems> Items { get; set; }
    }
}