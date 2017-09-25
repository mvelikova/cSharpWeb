using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopHierarchy
{
    public class Salesman
    {
        public Salesman()
        {
            this.Customers = new HashSet<Customer>();
        }

        public Salesman(string name) : this()
        {
            this.Name = name;
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}