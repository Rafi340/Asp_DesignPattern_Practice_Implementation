using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { -1, "create_user", "allowed", new Guid("f9019c06-a30a-4937-2a90-08dd9ac7ee99") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
