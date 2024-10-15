using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations.Data
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "data");

            migrationBuilder.CreateTable(
                name: "parameters",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<long>(type: "bigint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    unit = table.Column<string>(type: "text", nullable: true),
                    decimal_parameter_step = table.Column<decimal>(type: "numeric", nullable: true),
                    decimal_parameter_default_value = table.Column<decimal>(type: "numeric", nullable: true),
                    step = table.Column<int>(type: "integer", nullable: true),
                    default_value = table.Column<int>(type: "integer", nullable: true),
                    allowed_values = table.Column<List<string>>(type: "text[]", nullable: true),
                    text_parameter_default_value = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parameters", x => x.id);
                    table.ForeignKey(
                        name: "fk_parameters_parameters_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "data",
                        principalTable: "parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recipes",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_recipes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sample_steps",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sample_steps", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "parameter_values",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parameter_type = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    decimal_parameter_id = table.Column<Guid>(type: "uuid", nullable: true),
                    decimal_value = table.Column<decimal>(type: "numeric", nullable: true),
                    integer_parameter_id = table.Column<Guid>(type: "uuid", nullable: true),
                    integer_value = table.Column<int>(type: "integer", nullable: true),
                    text_parameter_id = table.Column<Guid>(type: "uuid", nullable: true),
                    text_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parameter_values", x => x.id);
                    table.ForeignKey(
                        name: "fk_parameter_values_parameters_decimal_parameter_id",
                        column: x => x.decimal_parameter_id,
                        principalSchema: "data",
                        principalTable: "parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parameter_values_parameters_integer_parameter_id",
                        column: x => x.integer_parameter_id,
                        principalSchema: "data",
                        principalTable: "parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_parameter_values_parameters_text_parameter_id",
                        column: x => x.text_parameter_id,
                        principalSchema: "data",
                        principalTable: "parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recipe_steps",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    recipe_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_steps", x => x.id);
                    table.ForeignKey(
                        name: "fk_recipe_steps_recipes_recipe_id",
                        column: x => x.recipe_id,
                        principalSchema: "data",
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "samples",
                schema: "data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recipe_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_samples", x => x.id);
                    table.ForeignKey(
                        name: "fk_samples_projects_project_id",
                        column: x => x.project_id,
                        principalSchema: "data",
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_samples_recipes_recipe_id",
                        column: x => x.recipe_id,
                        principalSchema: "data",
                        principalTable: "recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "base_step_parameter_value",
                schema: "data",
                columns: table => new
                {
                    base_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    values_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_base_step_parameter_value", x => new { x.base_step_id, x.values_id });
                    table.ForeignKey(
                        name: "fk_base_step_parameter_value_parameter_values_values_id",
                        column: x => x.values_id,
                        principalSchema: "data",
                        principalTable: "parameter_values",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sample_sample_step",
                schema: "data",
                columns: table => new
                {
                    sample_id = table.Column<Guid>(type: "uuid", nullable: false),
                    steps_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sample_sample_step", x => new { x.sample_id, x.steps_id });
                    table.ForeignKey(
                        name: "fk_sample_sample_step_sample_steps_steps_id",
                        column: x => x.steps_id,
                        principalSchema: "data",
                        principalTable: "sample_steps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sample_sample_step_samples_sample_id",
                        column: x => x.sample_id,
                        principalSchema: "data",
                        principalTable: "samples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sample_tag",
                schema: "data",
                columns: table => new
                {
                    sample_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tags_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sample_tag", x => new { x.sample_id, x.tags_id });
                    table.ForeignKey(
                        name: "fk_sample_tag_samples_sample_id",
                        column: x => x.sample_id,
                        principalSchema: "data",
                        principalTable: "samples",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_sample_tag_tags_tags_id",
                        column: x => x.tags_id,
                        principalSchema: "data",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_base_step_parameter_value_values_id",
                schema: "data",
                table: "base_step_parameter_value",
                column: "values_id");

            migrationBuilder.CreateIndex(
                name: "ix_parameter_values_decimal_parameter_id",
                schema: "data",
                table: "parameter_values",
                column: "decimal_parameter_id");

            migrationBuilder.CreateIndex(
                name: "ix_parameter_values_integer_parameter_id",
                schema: "data",
                table: "parameter_values",
                column: "integer_parameter_id");

            migrationBuilder.CreateIndex(
                name: "ix_parameter_values_text_parameter_id",
                schema: "data",
                table: "parameter_values",
                column: "text_parameter_id");

            migrationBuilder.CreateIndex(
                name: "ix_parameters_parent_id",
                schema: "data",
                table: "parameters",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_projects_name",
                schema: "data",
                table: "projects",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_recipe_steps_recipe_id",
                schema: "data",
                table: "recipe_steps",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "ix_recipes_name",
                schema: "data",
                table: "recipes",
                column: "name")
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:TsVectorConfig", "english");

            migrationBuilder.CreateIndex(
                name: "ix_sample_sample_step_steps_id",
                schema: "data",
                table: "sample_sample_step",
                column: "steps_id");

            migrationBuilder.CreateIndex(
                name: "ix_sample_tag_tags_id",
                schema: "data",
                table: "sample_tag",
                column: "tags_id");

            migrationBuilder.CreateIndex(
                name: "ix_samples_code_comment",
                schema: "data",
                table: "samples",
                columns: new[] { "code", "comment" })
                .Annotation("Npgsql:IndexMethod", "GIN")
                .Annotation("Npgsql:TsVectorConfig", "english");

            migrationBuilder.CreateIndex(
                name: "ix_samples_project_id",
                schema: "data",
                table: "samples",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_samples_recipe_id",
                schema: "data",
                table: "samples",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_name",
                schema: "data",
                table: "tags",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_step_parameter_value",
                schema: "data");

            migrationBuilder.DropTable(
                name: "recipe_steps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "sample_sample_step",
                schema: "data");

            migrationBuilder.DropTable(
                name: "sample_tag",
                schema: "data");

            migrationBuilder.DropTable(
                name: "parameter_values",
                schema: "data");

            migrationBuilder.DropTable(
                name: "sample_steps",
                schema: "data");

            migrationBuilder.DropTable(
                name: "samples",
                schema: "data");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "data");

            migrationBuilder.DropTable(
                name: "parameters",
                schema: "data");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "data");

            migrationBuilder.DropTable(
                name: "recipes",
                schema: "data");
        }
    }
}
