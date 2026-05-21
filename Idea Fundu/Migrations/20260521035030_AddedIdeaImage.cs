using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idea_Fundu.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdeaImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Ideas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Ideas");
        }
    }
}
