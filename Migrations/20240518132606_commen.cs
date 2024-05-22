using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class commen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FreindsId",
                table: "ForumUserForumUser");

            migrationBuilder.RenameColumn(
                name: "FreindsId",
                table: "ForumUserForumUser",
                newName: "FriendsId");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discussionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    visibility = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Discussions_discussionId",
                        column: x => x.discussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_discussionId",
                table: "Comments",
                column: "discussionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FriendsId",
                table: "ForumUserForumUser",
                column: "FriendsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FriendsId",
                table: "ForumUserForumUser");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.RenameColumn(
                name: "FriendsId",
                table: "ForumUserForumUser",
                newName: "FreindsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FreindsId",
                table: "ForumUserForumUser",
                column: "FreindsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
