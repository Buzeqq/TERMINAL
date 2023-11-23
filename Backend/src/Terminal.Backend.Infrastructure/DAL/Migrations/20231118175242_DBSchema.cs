using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DBSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementTag_Tags_TagsName",
                table: "MeasurementTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementTag",
                table: "MeasurementTag");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementTag_TagsName",
                table: "MeasurementTag");

            migrationBuilder.DropColumn(
                name: "TagsName",
                table: "MeasurementTag");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TagsId",
                table: "MeasurementTag",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementTag",
                table: "MeasurementTag",
                columns: new[] { "MeasurementId", "TagsId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementTag_TagsId",
                table: "MeasurementTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementTag_Tags_TagsId",
                table: "MeasurementTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasurementTag_Tags_TagsId",
                table: "MeasurementTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Name",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasurementTag",
                table: "MeasurementTag");

            migrationBuilder.DropIndex(
                name: "IX_MeasurementTag_TagsId",
                table: "MeasurementTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "MeasurementTag");

            migrationBuilder.AddColumn<string>(
                name: "TagsName",
                table: "MeasurementTag",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasurementTag",
                table: "MeasurementTag",
                columns: new[] { "MeasurementId", "TagsName" });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementTag_TagsName",
                table: "MeasurementTag",
                column: "TagsName");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasurementTag_Tags_TagsName",
                table: "MeasurementTag",
                column: "TagsName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
