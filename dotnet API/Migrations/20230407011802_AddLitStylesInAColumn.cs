using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class AddLitStylesInAColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Style",
                table: "Artistas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Style",
                table: "Artistas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
