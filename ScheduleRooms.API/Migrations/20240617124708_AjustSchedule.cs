using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUsers_Clients_ClientId",
                table: "ScheduleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUsers_Users_UserId",
                table: "ScheduleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers");

            migrationBuilder.DropColumn(
                name: "Manager",
                table: "ScheduleUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "ScheduleUsers");

            migrationBuilder.RenameTable(
                name: "ScheduleUsers",
                newName: "Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUsers_UserId",
                table: "Schedule",
                newName: "IX_Schedule_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUsers_ClientId",
                table: "Schedule",
                newName: "IX_Schedule_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Schedule",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ScheduleUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleUser", x => new { x.ScheduleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ScheduleUser_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleUser_UserId",
                table: "ScheduleUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Clients_ClientId",
                table: "Schedule",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Users_UserId",
                table: "Schedule",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Clients_ClientId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Users_UserId",
                table: "Schedule");

            migrationBuilder.DropTable(
                name: "ScheduleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedule",
                table: "Schedule");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "ScheduleUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_UserId",
                table: "ScheduleUsers",
                newName: "IX_ScheduleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ClientId",
                table: "ScheduleUsers",
                newName: "IX_ScheduleUsers_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ScheduleUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "ScheduleUsers",
                type: "VARCHAR(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "ScheduleUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUsers_Clients_ClientId",
                table: "ScheduleUsers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUsers_Users_UserId",
                table: "ScheduleUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
