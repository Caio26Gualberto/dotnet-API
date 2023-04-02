using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoSendMailColunaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SendMailId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ResetPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SendMailId",
                table: "Usuarios",
                column: "SendMailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_ResetPasswords_SendMailId",
                table: "Usuarios",
                column: "SendMailId",
                principalTable: "ResetPasswords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_ResetPasswords_SendMailId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "ResetPasswords");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SendMailId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SendMailId",
                table: "Usuarios");
        }
    }
}
