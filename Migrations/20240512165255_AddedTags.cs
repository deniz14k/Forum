using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class AddedTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_AspNetUsers_FreindsId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_AspNetUsers_UsersId",
                table: "Friend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend",
                table: "Friend");

            migrationBuilder.DropColumn(
                name: "category",
                table: "Discussions");

            migrationBuilder.RenameTable(
                name: "Friend",
                newName: "ForumUserForumUser");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_UsersId",
                table: "ForumUserForumUser",
                newName: "IX_ForumUserForumUser_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumUserForumUser",
                table: "ForumUserForumUser",
                columns: new[] { "FreindsId", "UsersId" });

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
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    discutionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionTag", x => new { x.TagsId, x.discutionsId });
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Discussions_discutionsId",
                        column: x => x.discutionsId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTag_discutionsId",
                table: "DiscussionTag",
                column: "discutionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FreindsId",
                table: "ForumUserForumUser",
                column: "FreindsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_UsersId",
                table: "ForumUserForumUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_FreindsId",
                table: "ForumUserForumUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumUserForumUser_AspNetUsers_UsersId",
                table: "ForumUserForumUser");

            migrationBuilder.DropTable(
                name: "DiscussionTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumUserForumUser",
                table: "ForumUserForumUser");

            migrationBuilder.RenameTable(
                name: "ForumUserForumUser",
                newName: "Friend");

            migrationBuilder.RenameIndex(
                name: "IX_ForumUserForumUser_UsersId",
                table: "Friend",
                newName: "IX_Friend_UsersId");

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Discussions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend",
                table: "Friend",
                columns: new[] { "FreindsId", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_AspNetUsers_FreindsId",
                table: "Friend",
                column: "FreindsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_AspNetUsers_UsersId",
                table: "Friend",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
