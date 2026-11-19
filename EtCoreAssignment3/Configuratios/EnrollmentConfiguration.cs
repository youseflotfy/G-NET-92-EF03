using EtCoreAssignment3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Configuratios
{
    internal class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new { e.StdId, e.CrsId });

            builder.HasOne(e => e.Course).WithMany(c => c.StudentsEnrollments).HasForeignKey(c => c.CrsId);

            builder.HasOne(e => e.Student).WithMany(s => s.CoursesEnrollment).HasForeignKey(s => s.StdId);

            builder.ToTable(t => t.HasCheckConstraint("CheckRangeProgressPercentage", "ProgressPercentage >= 0 and ProgressPercentage <=100 "));
            builder.Property(e => e.FinalGrade).HasPrecision(5, 2);
        }
    }
}
