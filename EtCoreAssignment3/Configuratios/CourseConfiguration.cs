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
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Price).HasPrecision(10, 2);
            builder.Property(c => c.CourseTItle).HasMaxLength(100).IsRequired();
            builder.HasData(
            new Course
            {
                Id = 1,
                CourseTItle = "EF Core Mastery",
                Price = 500m,
                Description = "Code-First with SQL Server",
                Level = CourseLevel.Intermediate,
                Flag = true,
                PublishedDate = new DateTime(2025, 2, 1),
                InstructorId = 1,
                CategoryId = 2
            }
                          );
        }
    }
}
