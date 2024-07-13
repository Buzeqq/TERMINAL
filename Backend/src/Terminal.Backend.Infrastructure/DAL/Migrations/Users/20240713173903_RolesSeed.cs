using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Terminal.Backend.Infrastructure.DAL.Migrations.Users
{
    /// <inheritdoc />
    public partial class RolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "users",
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "559e49c8-f017-4972-b42c-47ba70038b73", null, "Administrator", null },
                    { "73d5cbbc-3b79-4a6c-bff9-0190326588dd", null, "User", null },
                    { "c4172e25-403e-4582-a63e-4fde76f515ea", null, "Moderator", null },
                    { "f059ba24-56e2-4cf8-b25c-9665329966cb", null, "Guest", null }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "application_role",
                column: "id",
                values: new object[]
                {
                    "559e49c8-f017-4972-b42c-47ba70038b73",
                    "73d5cbbc-3b79-4a6c-bff9-0190326588dd",
                    "c4172e25-403e-4582-a63e-4fde76f515ea",
                    "f059ba24-56e2-4cf8-b25c-9665329966cb"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "users",
                table: "application_role",
                keyColumn: "id",
                keyValue: "559e49c8-f017-4972-b42c-47ba70038b73");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "application_role",
                keyColumn: "id",
                keyValue: "73d5cbbc-3b79-4a6c-bff9-0190326588dd");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "application_role",
                keyColumn: "id",
                keyValue: "c4172e25-403e-4582-a63e-4fde76f515ea");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "application_role",
                keyColumn: "id",
                keyValue: "f059ba24-56e2-4cf8-b25c-9665329966cb");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "559e49c8-f017-4972-b42c-47ba70038b73");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "73d5cbbc-3b79-4a6c-bff9-0190326588dd");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "c4172e25-403e-4582-a63e-4fde76f515ea");

            migrationBuilder.DeleteData(
                schema: "users",
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: "f059ba24-56e2-4cf8-b25c-9665329966cb");
        }
    }
}
