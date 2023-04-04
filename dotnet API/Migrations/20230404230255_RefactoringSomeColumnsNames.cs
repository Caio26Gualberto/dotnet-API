using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringSomeColumnsNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LocalNascimento",
                table: "Usuarios",
                newName: "BirthPlace");

            migrationBuilder.RenameColumn(
                name: "DataRegistro",
                table: "Usuarios",
                newName: "DataRecord");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "DataRecord",
                table: "Usuarios",
                newName: "DataRegistro");

            migrationBuilder.RenameColumn(
                name: "BirthPlace",
                table: "Usuarios",
                newName: "LocalNascimento");
        }
    }
}
