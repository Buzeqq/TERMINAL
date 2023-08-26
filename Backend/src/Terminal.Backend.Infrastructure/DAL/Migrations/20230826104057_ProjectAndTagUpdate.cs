using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAndTagUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Measurements_MeasurementId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MeasurementId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MeasurementId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "MeasurementTag",
                columns: table => new
                {
                    MeasurementId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementTag", x => new { x.MeasurementId, x.TagsName });
                    table.ForeignKey(
                        name: "FK_MeasurementTag_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementTag_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementTag_TagsName",
                table: "MeasurementTag",
                column: "TagsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementTag");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Name",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementId",
                table: "Tags",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MeasurementId",
                table: "Tags",
                column: "MeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Measurements_MeasurementId",
                table: "Tags",
                column: "MeasurementId",
                principalTable: "Measurements",
                principalColumn: "Id");
        }
    }
}
