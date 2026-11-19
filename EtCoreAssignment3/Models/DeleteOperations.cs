using EtCoreAssignment3.Configuratios;
using EtCoreAssignment3.Contexts;
using EtCoreAssignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace EtCoreAssignment3.Models;

internal static class DeleteOperations
{
    public static async Task UnenrollStudentAsync(OnlineLearningContext context, int studentId, int courseId)
    {
        try
        {
            var enrollment = await context.Enrollments.FindAsync(studentId, courseId);
            if (enrollment == null)
            {
                Console.WriteLine($"Refused: Enrollment for Student {studentId} and Course {courseId} does not exist.");
                return;
            }

            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();

            Console.WriteLine($"Success: Student {studentId} unenrolled from Course {courseId}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to unenroll student: {ex.Message} | Inner: {ex.InnerException?.Message}");
        }
    }

    public static async Task TryDeleteCategoryWithSubCategoriesAsync(OnlineLearningContext context, int categoryId)
    {
        try
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                Console.WriteLine($"Refused: Category with Id {categoryId} does not exist.");
                return;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            Console.WriteLine($"Success: Category {categoryId} deleted.");
        }
        catch (DbUpdateException ex)
        {
            var trackedEntry = context.ChangeTracker.Entries<Category>()
                .FirstOrDefault(e => e.Entity.Id == categoryId);

            if (trackedEntry != null)
            {
                trackedEntry.State = EntityState.Unchanged;
            }

            Console.WriteLine($"DbUpdateException caught while deleting Category {categoryId}: {ex.Message} | Inner: {ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Unexpected error while deleting Category {categoryId}: {ex.Message}");
        }
    }

    public static async Task DeleteCategorySafeAsync(OnlineLearningContext context, int categoryId)
    {
        try
        {
            var category = await context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                Console.WriteLine($"Refused: Category with Id {categoryId} does not exist.");
                return;
            }

            bool hasSubCategories = await context.Categories.AnyAsync(c => c.ParentCategoryId == categoryId);
            if (hasSubCategories)
            {
                Console.WriteLine($"Refused: Cannot delete Category {categoryId} because it has sub-categories.");
                return;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            Console.WriteLine($"Success: Category {categoryId} deleted safely.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to delete category safely: {ex.Message}");
        }
    }
     
    public static async Task DeleteCourseWithEnrollmentsAsync(OnlineLearningContext context, int courseId)
    {
        try
        {
            var course = await context.Courses.FindAsync(courseId);
            if (course == null)
            {
                Console.WriteLine($"Refused: Course with Id {courseId} does not exist.");
                return;
            }

            int enrollmentsBefore = await context.Enrollments.CountAsync(e => e.CrsId == courseId);
            Console.WriteLine($"Enrollments count for Course {courseId} before delete: {enrollmentsBefore}");

            context.Courses.Remove(course);
            await context.SaveChangesAsync();

            int enrollmentsAfter = await context.Enrollments.CountAsync(e => e.CrsId == courseId);
            Console.WriteLine($"Enrollments count for Course {courseId} after delete: {enrollmentsAfter}");

            Console.WriteLine($"Success: Course {courseId} deleted. Related enrollments were removed automatically due to Cascade Delete on Course and Enrollment.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: Failed to delete course: {ex.Message} | Inner: {ex.InnerException?.Message}");
        }
    }
}