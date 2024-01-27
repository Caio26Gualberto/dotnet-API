using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserTokenAssociationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioRefreshAssociacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRefreshAssociacao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRefreshAssociacao_RefreshToken",
                table: "UsuarioRefreshAssociacao",
                column: "RefreshToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioRefreshAssociacao");
        }
    }
}
