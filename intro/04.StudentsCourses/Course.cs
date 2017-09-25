using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _04.StudentsCourses
{
    public class Course
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<StudentCourse> StudentsCourses { get; set; }
    }
}