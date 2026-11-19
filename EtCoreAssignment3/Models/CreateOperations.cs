using EtCoreAssignment3.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtCoreAssignment3.Models
{
    internal static class CreateOperations
    {
        public static async Task AddInstructorAsync(OnlineLearningContext context)
        {
            try
            {
                var instructor = new Instructor
                {
                    FullName = "Mona Hassan",
                    EmailAddress = "mona.hassan@example.com",
                    ShortBio = "C# & ASP.NET Instructor",
                    JoinedDate = DateTime.Now
                };

                await context.Instructors.AddAsync(instructor);
                await context.SaveChangesAsync();

                Console.WriteLine($"Success Instructor added with Id: {instructor.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to add instructor: {ex.Message}");
            }
        }

        public static async Task AddCourseAsync(OnlineLearningContext context, int instructorId, int categoryId)
        {
            try
            {
                bool instructorExists = await context.Instructors.AnyAsync(i => i.Id == instructorId);
                bool categoryExists = await context.Categories.AnyAsync(c => c.Id == categoryId);

                if (!instructorExists || !categoryExists)
                {
                    Console.WriteLine($"[Refused] Cannot add course. Instructor ({instructorId}) or Category ({categoryId}) does not exist.");
                    return;
                }

                var course = new Course
                {
                    CourseTItle = "ASP.NET Core Web API",
                    Price = 650.00m,
                    Description = "C#",
                    Level = CourseLevel.Intermediate,
                    Flag = true,
                    PublishedDate = DateTime.Now,
                    InstructorId = instructorId,
                    CategoryId = categoryId
                };

                await context.Courses.AddAsync(course);
                await context.SaveChangesAsync();

                Console.WriteLine($"Success Course added with Id: {course.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to add course: {ex.Message}");
            }
        }

        public static async Task AddStudentsRangeAsync(OnlineLearningContext context)
        {
            try
            {
                var students = new List<Student>
            {
                new Student { FullName = "Youssef Tarek", EmailAddress = "youssef.tarek@example.com", DateOfBirth = new DateTime(2002, 4, 10) },
                new Student { FullName = "Salma Ibrahim", EmailAddress = "salma.ibrahim@example.com", DateOfBirth = new DateTime(2003, 8, 22) },
                new Student { FullName = "Karim Adel", EmailAddress = "karim.adel@example.com", DateOfBirth = new DateTime(2001, 12, 5) }
            };

                await context.Students.AddRangeAsync(students);
                await context.SaveChangesAsync();

                Console.WriteLine("Success 3 students added. Generated Ids: " + string.Join(", ", students.Select(s => s.Id)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to add students: {ex.Message}");
            }
        }
        public static async Task EnrollStudentAsync(OnlineLearningContext context, int studentId, int courseId)
        {
            try
            {
                bool studentExists = await context.Students.AnyAsync(s => s.Id == studentId);
                if (!studentExists)
                {
                    Console.WriteLine($"Refused Student with Id {studentId} does not exist.");
                    return;
                }

                var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                if (course == null)
                {
                    Console.WriteLine($"Refused Course with Id {courseId} does not exist.");
                    return;
                }

                if (!course.Flag)
                {
                    Console.WriteLine($"Refused Course '{course.CourseTItle}' is not published yet.");
                    return;
                }

                bool alreadyEnrolled = await context.Enrollments.AnyAsync(e => e.StdId == studentId && e.CrsId == courseId);
                if (alreadyEnrolled)
                {
                    Console.WriteLine($"Refused Student {studentId} is already enrolled in Course {courseId}.");
                    return;
                }

                var enrollment = new Enrollment
                {
                    StdId = studentId,
                    CrsId = courseId,
                    EnrollmentDate = DateTime.Now,
                    ProgressPercentage = 0m,
                    Status = EnrollmentStatus.Active,
                    FinalGrade = null
                };

                await context.Enrollments.AddAsync(enrollment);
                await context.SaveChangesAsync();

                Console.WriteLine($"Success Student {studentId} enrolled in Course {courseId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Failed to enroll student: {ex.Message}");
            }
        }
    }
}
