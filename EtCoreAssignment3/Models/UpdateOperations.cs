using EtCoreAssignment3.Configuratios;
using EtCoreAssignment3.Contexts;
using EtCoreAssignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace EtCoreAssignment3.Models;

internal static class UpdateOperations
{
    public static async Task ChangePriceAsync(OnlineLearningContext context, int courseId, decimal newPrice)
    {
        try
        {
            if (newPrice < 0)
            {
                Console.WriteLine("Refused: Price cannot be negative.");
                return;
            }

            var course = await context.Courses.FindAsync(courseId);
            if (course == null)
            {
                Console.WriteLine($"Refused: Course with Id {courseId} does not exist.");
                return;
            }

            decimal oldPrice = course.Price;
            course.Price = newPrice;

            await context.SaveChangesAsync();
            Console.WriteLine($"Success: Course {courseId} price updated from {oldPrice} to {newPrice}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to change course price: {ex.Message}");
        }
    }

    public static async Task UpdateProgressAsync(OnlineLearningContext context, int studentId, int courseId, decimal newProgress)
    {
        try
        {
            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine($"Refused: Progress value {newProgress} is outside 0-100.");
                return;
            }

            var enrollment = await context.Enrollments.FindAsync(studentId, courseId);
            if (enrollment == null)
            {
                Console.WriteLine($"Refused: Enrollment for Student {studentId} and Course {courseId} does not exist.");
                return;
            }

            enrollment.ProgressPercentage = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            await context.SaveChangesAsync();
            Console.WriteLine($"Success: Enrollment updated for Student {studentId} in Course {courseId} -> Progress: {enrollment.ProgressPercentage}%, Status: {enrollment.Status}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to update progress: {ex.Message}");
        }
    }

    public static async Task PublishCourseAsync(OnlineLearningContext context, int courseId)
    {
        try
        {
            var course = await context.Courses.FindAsync(courseId);
            if (course == null)
            {
                Console.WriteLine($"Refused: Course with Id {courseId} does not exist.");
                return;
            }

            course.Flag = true;
            course.PublishedDate = DateTime.Now;

            await context.SaveChangesAsync();
            Console.WriteLine($"Success: Course {courseId} ({course.CourseTItle}) is published on {course.PublishedDate}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to publish course: {ex.Message}");
        }
    }
}