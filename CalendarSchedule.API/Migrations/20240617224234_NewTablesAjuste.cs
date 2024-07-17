using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class NewTablesAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cellphone",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cellphone",
                table: "Users",
                type: "VARCHAR(20)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Cellphone",
                value: "44111111111");
        }
    }
}
