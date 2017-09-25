using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _0203.Departments
{
    public class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}