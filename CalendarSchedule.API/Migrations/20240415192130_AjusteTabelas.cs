using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllowChat",
                table: "ScheduleUsers",
                newName: "StatusSchedule");

            migrationBuilder.RenameColumn(
                name: "AllowCall",
                table: "ScheduleUsers",
                newName: "MeetingType");

            migrationBuilder.AddColumn<bool>(
                name: "Manager",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "StatusSchedule",
                table: "ScheduleUsers",
                newName: "AllowChat");

            migrationBuilder.RenameColumn(
                name: "MeetingType",
                table: "ScheduleUsers",
                newName: "AllowCall");
        }
    }
}
