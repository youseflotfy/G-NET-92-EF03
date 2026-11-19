using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal class Enrollment
    {
        public DateTime EnrollmentDate { get; set; }
        public decimal ProgressPercentage { get; set; }
        public EnrollmentStatus Status { get; set; }
        public decimal? FinalGrade { get; set; }
        public Course Course { get; set; } = null!;
        public int CrsId { get; set; }
        public Student Student { get; set; }=null!;
        public int StdId { get; set; }
    }
}
