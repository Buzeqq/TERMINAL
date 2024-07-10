using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations.Users
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                schema: "users",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                schema: "users",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_roles",
                schema: "users",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "role_id",
                schema: "users",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "users",
                table: "AspNetRoles",
                column: "id");

            migrationBuilder.CreateTable(
                name: "application_role",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_application_role_asp_net_roles_id",
                        column: x => x.id,
                        principalSchema: "users",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_role_id",
                schema: "users",
                table: "AspNetUsers",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_roles_role_id",
                schema: "users",
                table: "AspNetUsers",
                column: "role_id",
                principalSchema: "users",
                principalTable: "application_role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_roles_role_id",
                schema: "users",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "application_role",
                schema: "users");

            migrationBuilder.DropIndex(
                name: "ix_asp_net_users_role_id",
                schema: "users",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "users",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "role_id",
                schema: "users",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_roles",
                schema: "users",
                table: "AspNetRoles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                column: "role_id",
                schema: "users",
                table: "AspNetRoleClaims",
                principalSchema: "users",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                column: "role_id",
                schema: "users",
                table: "AspNetUserRoles",
                principalSchema: "users",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
