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
    internal class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasMany(I => I.Courses).WithOne(C => C.Instructor).HasForeignKey(C => C.InstructorId);

            builder.Property(i => i.FullName).HasMaxLength(200).IsRequired();
            builder.Property(i => i.EmailAddress).HasMaxLength(200).IsRequired();

            builder.HasData(
                            new Instructor
                            {
                                Id = 1,
                                FullName = "Ahmed Ali",
                                EmailAddress = "ahmed@example.com",
                                ShortBio = ".NET Instructor",
                                JoinedDate = new DateTime(2025, 1, 10)
                            }
                          );


        }
    }
}
