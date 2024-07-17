using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableScheduleUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Particular",
                table: "ScheduleUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Particular",
                table: "ScheduleUsers");
        }
    }
}
