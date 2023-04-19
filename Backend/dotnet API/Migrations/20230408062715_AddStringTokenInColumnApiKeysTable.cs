using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class AddStringTokenInColumnApiKeysTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenString",
                table: "ApiKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenString",
                table: "ApiKeys");
        }
    }
}
