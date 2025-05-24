using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.Web.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("66cb7e8a-0d51-42c0-82dd-dcc1a80491d9"), "4/19/2025 2:03:04 AM", "HR", "HR" },
                    { new Guid("7870041d-77ef-4788-bae3-6d092e4e053b"), "4/19/2025 1:02:01 AM", "Admin", "ADMIN" },
                    { new Guid("b0cba364-772b-4c50-ab32-4af1a9e5dc84"), "4/19/2025 2:03:04 AM", "Author", "AUTHOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("66cb7e8a-0d51-42c0-82dd-dcc1a80491d9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7870041d-77ef-4788-bae3-6d092e4e053b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b0cba364-772b-4c50-ab32-4af1a9e5dc84"));
        }
    }
}
