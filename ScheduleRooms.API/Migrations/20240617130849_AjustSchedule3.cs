using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustSchedule3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Clients_ClientId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUser_Schedule_ScheduleId",
                table: "ScheduleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUser_Users_UserId",
                table: "ScheduleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleUser",
                table: "ScheduleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.RenameTable(
                name: "ScheduleUser",
                newName: "ScheduleUsers");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUser_UserId",
                table: "ScheduleUsers",
                newName: "IX_ScheduleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ClientId",
                table: "Schedules",
                newName: "IX_Schedules_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers",
                columns: new[] { "ScheduleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Clients_ClientId",
                table: "Schedules",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUsers_Schedules_ScheduleId",
                table: "ScheduleUsers",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUsers_Users_UserId",
                table: "ScheduleUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Clients_ClientId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUsers_Schedules_ScheduleId",
                table: "ScheduleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUsers_Users_UserId",
                table: "ScheduleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "ScheduleUsers",
                newName: "ScheduleUser");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUsers_UserId",
                table: "ScheduleUser",
                newName: "IX_ScheduleUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ClientId",
                table: "Schedule",
                newName: "IX_Schedule_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleUser",
                table: "ScheduleUser",
                columns: new[] { "ScheduleId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Clients_ClientId",
                table: "Schedule",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUser_Schedule_ScheduleId",
                table: "ScheduleUser",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUser_Users_UserId",
                table: "ScheduleUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
