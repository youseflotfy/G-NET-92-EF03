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
    internal class StudentConfigyration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.FullName).HasMaxLength(200).IsRequired();
            builder.Property(s => s.EmailAddress).HasMaxLength(200).IsRequired();
        }
    }
}
