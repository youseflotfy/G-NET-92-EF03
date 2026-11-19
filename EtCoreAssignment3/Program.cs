using EtCoreAssignment3.Contexts;
using EtCoreAssignment3.Models;

namespace EfCore 
{
    internal class MyClass
    {
        static async Task Main(string[] args) 
        {
            using OnlineLearningContext context = new OnlineLearningContext();


            //await CreateOperations.AddInstructorAsync(context);
            //await CreateOperations.AddCourseAsync(context, instructorId: 1, categoryId: 2);
            //await CreateOperations.AddStudentsRangeAsync(context);
            //await CreateOperations.EnrollStudentAsync(context, studentId: 1, courseId: 1);


            //Console.WriteLine("====================================================");
            //    await ReadOperations.FindCourseAsync(context, courseId: 1, courseTitle: "EF Core Mastery");
            //    await ReadOperations.FilterCoursesAsync(context, minPrice: 300m, maxPrice: 700m);
            //    await ReadOperations.PrintSubCategoriesAndTopLevelAsync(context, parentCategoryId: 1);
            //    await ReadOperations.PrintCourseRosterAsync(context, courseId: 1);


            //Console.WriteLine("====================================================");

            //await UpdateOperations.ChangePriceAsync(context, courseId: 1, newPrice: 550m);
            //await UpdateOperations.UpdateProgressAsync(context, studentId: 1, courseId: 1, newProgress: 100m);
            //await UpdateOperations.PublishCourseAsync(context, courseId: 1);

            Console.WriteLine("====================================================");

            await DeleteOperations.TryDeleteCategoryWithSubCategoriesAsync(context, categoryId: 1);
            await DeleteOperations.DeleteCategorySafeAsync(context, categoryId: 1);
            await DeleteOperations.UnenrollStudentAsync(context, studentId: 1, courseId: 1);
            await DeleteOperations.DeleteCourseWithEnrollmentsAsync(context, courseId: 1);

        }
    }
}