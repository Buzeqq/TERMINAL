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
                    { "e4804bf0-f521-42ab-b728-9fced1829468", null, "Guest", "GUEST", "ApplicationRole" },
                    { "f1c13625-7212-4e69-bdd5-daba8dd1c4bf", null, "Moderator", "MODERATOR", "ApplicationRole" },
                    { "f431f892-340a-491a-93a7-9822fd9aefef", null, "Administrator", "ADMINISTRATOR", "ApplicationRole" },
                    { "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641", null, "User", "USER", "ApplicationRole" }
                });

            migrationBuilder.InsertData(
                schema: "users",
                table: "AspNetRoleClaims",
                columns: new[] { "id", "claim_type", "claim_value", "role_id" },
                values: new object[,]
                {
                    { 1, "UserRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 2, "UserWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 3, "UserUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 4, "UserDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 5, "ProjectRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 6, "ProjectWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 7, "ProjectUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 8, "ProjectDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 9, "ParameterRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 10, "ParameterWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 11, "ParameterUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 12, "ParameterDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 13, "ProjectRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 14, "ProjectWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 15, "ProjectUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 16, "ProjectDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 17, "TagRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 18, "TagWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 19, "TagUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 20, "TagDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 21, "RecipeRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 22, "RecipeWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 23, "RecipeUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 24, "RecipeDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 25, "SampleRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 26, "SampleWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 27, "SampleUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 28, "SampleDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 29, "StepRead", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 30, "StepWrite", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 31, "StepUpdate", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 32, "StepDelete", "true", "f431f892-340a-491a-93a7-9822fd9aefef" },
                    { 33, "UserRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 34, "ProjectRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 35, "ProjectRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 36, "ProjectRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 37, "ProjectWrite", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 38, "ProjectUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 39, "ProjectDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 40, "RecipeRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 41, "RecipeUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 42, "RecipeUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 43, "RecipeDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 44, "TagRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 45, "TagWrite", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 46, "TagUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 47, "TagDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 48, "SampleRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 49, "SampleWrite", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 50, "SampleUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 51, "SampleDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 52, "ParameterRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 53, "ParameterWrite", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 54, "ParameterUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 55, "ParameterDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 56, "StepRead", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 57, "StepWrite", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 58, "StepUpdate", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 59, "StepDelete", "true", "f1c13625-7212-4e69-bdd5-daba8dd1c4bf" },
                    { 60, "ProjectRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 61, "RecipeRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 62, "RecipeWrite", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 63, "TagRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 64, "TagWrite", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 65, "SampleRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 66, "SampleWrite", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 67, "ParameterRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 68, "StepRead", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" },
                    { 69, "StepWrite", "true", "fe5ab6b3-0d7f-4eef-b7cb-c17b05697641" }
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
