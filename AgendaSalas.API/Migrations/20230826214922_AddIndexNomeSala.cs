using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaSalas.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexNomeSala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Salas_Nome",
                table: "Salas",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Salas_Nome",
                table: "Salas");
        }
    }
}
