using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EtCoreAssignment3.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 1, "All programming courses", "Programming", null });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "EmailAddress", "FullName", "JoinedDate", "ShortBio" },
                values: new object[] { 1, "ahmed@example.com", "Ahmed Ali", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), ".NET Instructor" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 2, "Backend development", "C# & .NET", 1 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CourseTItle", "Description", "Flag", "InstructorId", "Level", "Price", "PublishedDate" },
                values: new object[] { 1, 2, "EF Core Mastery", "Code-First with SQL Server", true, 1, 1, 500m, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
