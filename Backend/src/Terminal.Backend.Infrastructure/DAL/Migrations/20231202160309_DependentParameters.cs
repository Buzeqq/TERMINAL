using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terminal.Backend.Infrastructure.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DependentParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "Parameters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_ParentId",
                table: "Parameters",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Parameters_ParentId",
                table: "Parameters",
                column: "ParentId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Parameters_ParentId",
                table: "Parameters");

            migrationBuilder.DropIndex(
                name: "IX_Parameters_ParentId",
                table: "Parameters");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Parameters");
        }
    }
}
