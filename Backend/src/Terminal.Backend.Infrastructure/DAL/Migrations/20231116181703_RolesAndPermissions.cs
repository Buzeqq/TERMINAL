using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleValue",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "UserRead" },
                    { 2, "UserWrite" },
                    { 3, "UserUpdate" },
                    { 4, "UserDelete" },
                    { 5, "ProjectRead" },
                    { 6, "ProjectWrite" },
                    { 7, "ProjectUpdate" },
                    { 8, "ProjectDelete" },
                    { 9, "RecipeRead" },
                    { 10, "RecipeWrite" },
                    { 11, "RecipeUpdate" },
                    { 12, "RecipeDelete" },
                    { 13, "TagRead" },
                    { 14, "TagWrite" },
                    { 15, "TagUpdate" },
                    { 16, "TagDelete" },
                    { 17, "MeasurementRead" },
                    { 18, "MeasurementWrite" },
                    { 19, "MeasurementUpdate" },
                    { 20, "MeasurementDelete" },
                    { 21, "ParameterRead" },
                    { 22, "ParameterWrite" },
                    { 23, "ParameterUpdate" },
                    { 24, "ParameterDelete" },
                    { 25, "StepRead" },
                    { 26, "StepWrite" },
                    { 27, "StepUpdate" },
                    { 28, "StepDelete" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Value", "Name" },
                values: new object[,]
                {
                    { 0, "Guest" },
                    { 1, "Registered" },
                    { 2, "Moderator" },
                    { 3, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 5, 1 },
                    { 5, 2 },
                    { 5, 3 },
                    { 6, 2 },
                    { 6, 3 },
                    { 7, 2 },
                    { 7, 3 },
                    { 8, 2 },
                    { 8, 3 },
                    { 9, 1 },
                    { 9, 2 },
                    { 9, 3 },
                    { 10, 2 },
                    { 10, 3 },
                    { 11, 2 },
                    { 11, 3 },
                    { 12, 2 },
                    { 12, 3 },
                    { 13, 1 },
                    { 13, 2 },
                    { 13, 3 },
                    { 14, 1 },
                    { 14, 2 },
                    { 14, 3 },
                    { 15, 1 },
                    { 15, 2 },
                    { 15, 3 },
                    { 16, 2 },
                    { 16, 3 },
                    { 17, 1 },
                    { 17, 2 },
                    { 17, 3 },
                    { 18, 1 },
                    { 18, 2 },
                    { 18, 3 },
                    { 19, 1 },
                    { 19, 2 },
                    { 19, 3 },
                    { 20, 2 },
                    { 20, 3 },
                    { 21, 1 },
                    { 21, 2 },
                    { 21, 3 },
                    { 22, 2 },
                    { 22, 3 },
                    { 23, 2 },
                    { 23, 3 },
                    { 24, 2 },
                    { 24, 3 },
                    { 25, 1 },
                    { 25, 2 },
                    { 25, 3 },
                    { 26, 1 },
                    { 26, 2 },
                    { 26, 3 },
                    { 27, 1 },
                    { 27, 2 },
                    { 27, 3 },
                    { 28, 2 },
                    { 28, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleValue",
                table: "Users",
                column: "RoleValue");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_RoleValue",
                table: "Users",
                column: "RoleValue",
                principalTable: "Role",
                principalColumn: "Value",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_RoleValue",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleValue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleValue",
                table: "Users");
        }
    }
}
