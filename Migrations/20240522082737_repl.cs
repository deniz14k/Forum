using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class repl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "parentId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_parentId",
                table: "Comments",
                column: "parentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_parentId",
                table: "Comments",
                column: "parentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_parentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_parentId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "parentId",
                table: "Comments");
        }
    }
}
