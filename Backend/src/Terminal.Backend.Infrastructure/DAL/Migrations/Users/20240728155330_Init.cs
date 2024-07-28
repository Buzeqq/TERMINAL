using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Terminal.Backend.Infrastructure.DAL.Migrations.Users
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    role_type = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "users",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "users",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "users",
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "users",
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name", "role_type" },
                values: new object[,]
                {
                    { "6efd4890-0510-44e9-8131-fdcf16a854c8", null, "Moderator", "MODERATOR", "ApplicationRole" },
                    { "85ef61ce-8039-4d5e-80a0-845e2d8d194c", null, "Guest", "GUEST", "ApplicationRole" },
                    { "a5d79af6-8d12-4898-9097-2229c7e72434", null, "User", "USER", "ApplicationRole" },
                    { "be624ab5-9965-4482-aa73-6615d220580d", null, "Administrator", "ADMINISTRATOR", "ApplicationRole" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "AspNetRoleClaims",
                columns: new[] { "id", "claim_type", "claim_value", "role_id" },
                values: new object[,]
                {
                    { 1, "UserRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 2, "UserWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 3, "UserUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 4, "UserDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 5, "ProjectRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 6, "ProjectWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 7, "ProjectUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 8, "ProjectDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 9, "ParameterRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 10, "ParameterWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 11, "ParameterUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 12, "ParameterDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 13, "ProjectRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 14, "ProjectWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 15, "ProjectUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 16, "ProjectDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 17, "TagRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 18, "TagWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 19, "TagUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 20, "TagDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 21, "RecipeRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 22, "RecipeWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 23, "RecipeUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 24, "RecipeDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 25, "SampleRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 26, "SampleWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 27, "SampleUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 28, "SampleDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 29, "StepRead", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 30, "StepWrite", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 31, "StepUpdate", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 32, "StepDelete", "true", "be624ab5-9965-4482-aa73-6615d220580d" },
                    { 33, "UserRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 34, "ProjectRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 35, "ProjectRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 36, "ProjectRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 37, "ProjectWrite", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 38, "ProjectUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 39, "ProjectDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 40, "RecipeRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 41, "RecipeUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 42, "RecipeUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 43, "RecipeDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 44, "TagRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 45, "TagWrite", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 46, "TagUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 47, "TagDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 48, "SampleRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 49, "SampleWrite", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 50, "SampleUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 51, "SampleDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 52, "ParameterRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 53, "ParameterWrite", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 54, "ParameterUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 55, "ParameterDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 56, "StepRead", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 57, "StepWrite", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 58, "StepUpdate", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 59, "StepDelete", "true", "6efd4890-0510-44e9-8131-fdcf16a854c8" },
                    { 60, "ProjectRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 61, "RecipeRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 62, "RecipeWrite", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 63, "TagRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 64, "TagWrite", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 65, "SampleRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 66, "SampleWrite", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 67, "ParameterRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 68, "StepRead", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" },
                    { 69, "StepWrite", "true", "a5d79af6-8d12-4898-9097-2229c7e72434" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                schema: "users",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "users",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                schema: "users",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                schema: "users",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                schema: "users",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "users",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "users",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "users");
        }
    }
}
