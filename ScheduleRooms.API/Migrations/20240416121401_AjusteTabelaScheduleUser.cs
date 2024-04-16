using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelaScheduleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "ScheduleUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "ScheduleUsers");
        }
    }
}
