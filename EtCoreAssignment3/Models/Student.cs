using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal class Student
    {

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public ICollection<Enrollment> CoursesEnrollment { get; set; } = new HashSet<Enrollment>();
    }
}

