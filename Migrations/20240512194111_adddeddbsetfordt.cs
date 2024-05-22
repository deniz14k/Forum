using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class adddeddbsetfordt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionTag_Discussions_discussionId",
                table: "DiscussionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionTag_Tags_tagId",
                table: "DiscussionTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionTag",
                table: "DiscussionTag");

            migrationBuilder.RenameTable(
                name: "DiscussionTag",
                newName: "DiscussionTags");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionTag_tagId",
                table: "DiscussionTags",
                newName: "IX_DiscussionTags_tagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionTags",
                table: "DiscussionTags",
                columns: new[] { "discussionId", "tagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionTags_Discussions_discussionId",
                table: "DiscussionTags",
                column: "discussionId",
                principalTable: "Discussions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionTags_Tags_tagId",
                table: "DiscussionTags",
                column: "tagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionTags_Discussions_discussionId",
                table: "DiscussionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_DiscussionTags_Tags_tagId",
                table: "DiscussionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DiscussionTags",
                table: "DiscussionTags");

            migrationBuilder.RenameTable(
                name: "DiscussionTags",
                newName: "DiscussionTag");

            migrationBuilder.RenameIndex(
                name: "IX_DiscussionTags_tagId",
                table: "DiscussionTag",
                newName: "IX_DiscussionTag_tagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DiscussionTag",
                table: "DiscussionTag",
                columns: new[] { "discussionId", "tagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionTag_Discussions_discussionId",
                table: "DiscussionTag",
                column: "discussionId",
                principalTable: "Discussions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscussionTag_Tags_tagId",
                table: "DiscussionTag",
                column: "tagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
