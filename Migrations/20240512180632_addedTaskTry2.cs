using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class addedTaskTry2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionTag",
                columns: table => new
                {
                    discussionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionTag", x => new { x.discussionId, x.tagId });
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Discussions_discussionId",
                        column: x => x.discussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Tags_tagId",
                        column: x => x.tagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTag_tagId",
                table: "DiscussionTag",
                column: "tagId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "Tags");

           
        }
    }
}
