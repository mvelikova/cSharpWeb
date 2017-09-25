using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _0203.Departments
{
    public class Employee
    {
        public Employee()
        {
            this.Subordinates = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public Employee Manager { get; set; }
        public int? ManagerId { get; set; }

        public ICollection<Employee> Subordinates { get; set; }
    }
}