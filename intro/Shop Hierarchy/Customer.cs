using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy
{
    public class Customer
    {
        public Customer()
        {
            this.Orders = new HashSet<Order>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public int? SalesmanId { get; set; }
        public Salesman Salesman { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}