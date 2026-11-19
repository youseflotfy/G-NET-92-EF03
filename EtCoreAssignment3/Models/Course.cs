using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal class Course
    {
        public int Id { get; set; }
        public string CourseTItle { get; set; } = null!;

        public decimal Price { get; set; }
        public string? Description{ get; set; }
        public CourseLevel Level { get; set; }
        public bool Flag { get; set; }

        public DateTime? PublishedDate { get; set; }

        public Instructor Instructor { get; set; } = null!;
        public int InstructorId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<Enrollment> StudentsEnrollments { get; set; } = new HashSet<Enrollment>();
    }
}
