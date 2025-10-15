using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace redditWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init8NuEndnuVildere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentDownvotes",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentUpvotes",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentDownvotes",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentUpvotes",
                table: "Comments");
        }
    }
}
