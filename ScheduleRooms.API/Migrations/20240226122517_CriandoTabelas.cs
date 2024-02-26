using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUser_Client_ClientId",
                table: "ScheduleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleUser_User_UserId",
                table: "ScheduleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleUser",
                table: "ScheduleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ScheduleUser",
                newName: "SchedueleUsers");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUser_UserId",
                table: "SchedueleUsers",
                newName: "IX_SchedueleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduleUser_ClientId",
                table: "SchedueleUsers",
                newName: "IX_SchedueleUsers_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchedueleUsers",
                table: "SchedueleUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchedueleUsers_Clients_ClientId",
                table: "SchedueleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SchedueleUsers_Users_UserId",
                table: "SchedueleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchedueleUsers",
                table: "SchedueleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "SchedueleUsers",
                newName: "ScheduleUser");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameIndex(
                name: "IX_SchedueleUsers_UserId",
                table: "ScheduleUser",
                newName: "IX_ScheduleUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SchedueleUsers_ClientId",
                table: "ScheduleUser",
                newName: "IX_ScheduleUser_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleUser",
                table: "ScheduleUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUser_Client_ClientId",
                table: "ScheduleUser",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleUser_User_UserId",
                table: "ScheduleUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
