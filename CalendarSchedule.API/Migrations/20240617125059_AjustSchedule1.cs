using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustSchedule1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Users_UserId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Schedule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Schedule",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_UserId",
                table: "Schedule",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Users_UserId",
                table: "Schedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
