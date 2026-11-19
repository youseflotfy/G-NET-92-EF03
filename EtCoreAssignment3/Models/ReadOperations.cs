using EtCoreAssignment3.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal static class ReadOperations
    {
        public static async Task FindCourseAsync(OnlineLearningContext context, int courseId, string courseTitle)
        {
            try
            {
                var courseById = await context.Courses.FindAsync(courseId);
                if (courseById == null)
                {
                    Console.WriteLine($"Find by Id={courseId}: not found");
                }
                else
                {
                    Console.WriteLine($"Find by Id={courseId}: Found {courseById.CourseTItle} | Price: {courseById.Price}");
                }

                var courseByTitle = await context.Courses
                    .FirstOrDefaultAsync(c => c.CourseTItle == courseTitle);

                if (courseByTitle == null)
                {
                    Console.WriteLine($"FirstOrDefault Title='{courseTitle}': not found");
                }
                else
                {
                    Console.WriteLine($"FirstOrDefault Title='{courseTitle}': Found Id={courseByTitle.Id}, Price={courseByTitle.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to find course: {ex.Message}");
            }
        }

        public static async Task FilterCoursesAsync(OnlineLearningContext context, decimal minPrice, decimal maxPrice)
        {
            try
            {
                var filteredCourses = await context.Courses
                    .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
                    .OrderByDescending(c => c.Price)
                    .Select(c => new
                    {
                        c.CourseTItle,
                        c.Level,
                        c.Price
                    })
                    .ToListAsync();

                Console.WriteLine($"Filtered Courses between {minPrice} and {maxPrice} - Count: {filteredCourses.Count}");
                foreach (var item in filteredCourses)
                {
                    Console.WriteLine($"- Title: {item.CourseTItle} | Level: {item.Level} | Price: {item.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to filter courses: {ex.Message}");
            }
        }

        public static async Task PrintSubCategoriesAndTopLevelAsync(OnlineLearningContext context, int parentCategoryId)
        {
            try
            {
                var subCategories = await context.Categories
                    .Where(c => c.ParentCategoryId == parentCategoryId)
                    .ToListAsync();

                Console.WriteLine($"Direct Sub-categories of Category Id={parentCategoryId} - Count: {subCategories.Count}");
                if (subCategories.Count == 0)
                {
                    Console.WriteLine("- No sub-categories found.");
                }
                else
                {
                    foreach (var sub in subCategories)
                    {
                        Console.WriteLine($"- Id: {sub.Id} | Name: {sub.Name}");
                    }
                }

                var topLevelCategories = await context.Categories
                    .Where(c => c.ParentCategoryId == null)
                    .ToListAsync();

                Console.WriteLine($"Top-level Categories - Count: {topLevelCategories.Count}");
                foreach (var top in topLevelCategories)
                {
                    Console.WriteLine($"- Id: {top.Id} | Name: {top.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to load categories: {ex.Message}");
            }
        }

        public static async Task PrintCourseRosterAsync(OnlineLearningContext context, int courseId)
        {
            try
            {
                bool courseExists = await context.Courses.AnyAsync(c => c.Id == courseId);
                if (!courseExists)
                {
                    Console.WriteLine($"Course Roster: Course with Id={courseId} not found.");
                    return;
                }

                var enrolledStudentIds = await context.Enrollments
                    .Where(e => e.CrsId == courseId)
                    .Select(e => e.StdId)
                    .ToListAsync();

                var students = await context.Students
                    .Where(s => enrolledStudentIds.Contains(s.Id))
                    .ToListAsync();

                Console.WriteLine($"Course Roster for Course Id={courseId} - Enrolled Students Count: {students.Count}");
                if (students.Count == 0)
                {
                    Console.WriteLine("- No students enrolled.");
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"- {student.FullName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to load course roster: {ex.Message}");
            }
        }
    }
}
