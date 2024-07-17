using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Schedules",
                type: "int",
                nullable: true);
        }
    }
}
