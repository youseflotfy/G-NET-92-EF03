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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne(pc => pc.ParentCategory).WithMany().HasForeignKey(pc =>pc.ParentCategoryId).OnDelete(DeleteBehavior.Restrict);

             builder.HasMany(c => c.Courses)
            .WithOne(co => co.Category)
            .HasForeignKey(co => co.CategoryId);

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.HasData(
                            new Category
                            {
                                Id = 1,
                                Name = "Programming",
                                Description = "All programming courses",
                                ParentCategoryId = null
                            },
                            new Category
                            {
                                Id = 2,
                                Name = "C# & .NET",
                                Description = "Backend development",
                                ParentCategoryId = 1 
                            }
                        );
        }
    }
}
