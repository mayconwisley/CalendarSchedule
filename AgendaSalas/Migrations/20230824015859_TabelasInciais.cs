using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaSalas.Migrations
{
    /// <inheritdoc />
    public partial class TabelasInciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SalaReuniao = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Ramal = table.Column<string>(type: "VARCHAR(5)", nullable: true),
                    Descricao = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reunioes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "VARCHAR(1000)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PermitirLigar = table.Column<bool>(type: "INTEGER", nullable: false),
                    PermitirChamar = table.Column<bool>(type: "INTEGER", nullable: false),
                    SalaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reunioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reunioes_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reunioes_SalaId",
                table: "Reunioes",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reunioes");

            migrationBuilder.DropTable(
                name: "Salas");
        }
    }
}
