using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                type: "VARCHAR(50)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Prospection",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Prospection",
                table: "Clients");
        }
    }
}
