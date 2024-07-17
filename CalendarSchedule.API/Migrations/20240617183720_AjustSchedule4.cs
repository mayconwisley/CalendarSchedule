using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustSchedule4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRooms_Rooms_RoomId",
                table: "ScheduleRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleRooms",
                table: "ScheduleRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "ScheduleRooms",
                newName: "ScheduleRoom");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleRooms_RoomId",
                table: "ScheduleRoom",
                newName: "IX_ScheduleRoom_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_Name",
                table: "Room",
                newName: "IX_Room_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleRoom",
                table: "ScheduleRoom",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRoom_Room_RoomId",
                table: "ScheduleRoom",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleRoom_Room_RoomId",
                table: "ScheduleRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleRoom",
                table: "ScheduleRoom");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "ScheduleRoom",
                newName: "ScheduleRooms");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleRoom_RoomId",
                table: "ScheduleRooms",
                newName: "IX_ScheduleRooms_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_Name",
                table: "Rooms",
                newName: "IX_Rooms_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleRooms",
                table: "ScheduleRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRooms_Rooms_RoomId",
                table: "ScheduleRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
