using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarSchedule.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNomeTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchedueleUsers_Clients_ClientId",
                table: "SchedueleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SchedueleUsers_Users_UserId",
                table: "SchedueleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchedueleUsers",
                table: "SchedueleUsers");

            migrationBuilder.RenameTable(
                name: "SchedueleUsers",
                newName: "ScheduleUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SchedueleUsers_UserId",
                table: "ScheduleUsers",
                newName: "IX_ScheduleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SchedueleUsers_ClientId",
                table: "ScheduleUsers",
                newName: "IX_ScheduleUsers_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUsers_Clients_ClientId",
                table: "ScheduleUsers",
                column: "ClientId",
                principalTable: "Clients",
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
                name: "FK_ScheduleUsers_Clients_ClientId",
                table: "ScheduleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUsers_Users_UserId",
                table: "ScheduleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleUsers",
                table: "ScheduleUsers");

            migrationBuilder.RenameTable(
                name: "ScheduleUsers",
                newName: "SchedueleUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUsers_UserId",
                table: "SchedueleUsers",
                newName: "IX_SchedueleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUsers_ClientId",
                table: "SchedueleUsers",
                newName: "IX_SchedueleUsers_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchedueleUsers",
                table: "SchedueleUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchedueleUsers_Clients_ClientId",
                table: "SchedueleUsers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchedueleUsers_Users_UserId",
                table: "SchedueleUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
