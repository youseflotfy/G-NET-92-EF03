using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;
        public string? ShortBio { get; set; }

        public DateTime JoinedDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

    }
}
