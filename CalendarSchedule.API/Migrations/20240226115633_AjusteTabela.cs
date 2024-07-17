using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "ScheduleRooms");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_RoomId",
                table: "ScheduleRooms",
                newName: "IX_ScheduleRooms_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleRooms",
                table: "ScheduleRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRooms_Rooms_RoomId",
                table: "ScheduleRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRooms_Rooms_RoomId",
                table: "ScheduleRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleRooms",
                table: "ScheduleRooms");

            migrationBuilder.RenameTable(
                name: "ScheduleRooms",
                newName: "Schedules");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleRooms_RoomId",
                table: "Schedules",
                newName: "IX_Schedules_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedules",
                table: "Schedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Rooms_RoomId",
                table: "Schedules",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
