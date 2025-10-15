using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace redditWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentTitle",
                table: "Comments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentTitle",
                table: "Comments",
                type: "TEXT",
                nullable: true);
        }
    }
}
