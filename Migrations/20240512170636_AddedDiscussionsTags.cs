using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsUnite_II.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscussionsTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionTag");

            migrationBuilder.CreateTable(
                name: "DiscussionsTags",
                columns: table => new
                {
                    DiscussionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionsTags", x => new { x.DiscussionId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DiscussionsTags_Discussions_DiscussionId",
                        column: x => x.DiscussionId,
                        principalTable: "Discussions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionsTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionsTags_TagsId",
                table: "DiscussionsTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionsTags");

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
        }
    }
}
