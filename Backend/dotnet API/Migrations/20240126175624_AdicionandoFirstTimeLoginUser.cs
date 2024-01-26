using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoFirstTimeLoginUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstTimeLogin",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstTimeLogin",
                table: "Usuarios");
        }
    }
}
