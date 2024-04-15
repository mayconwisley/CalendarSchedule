using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleRooms.API.Migrations
{
    /// <inheritdoc />
    public partial class UsernamePassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "VARCHAR(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "VARCHAR(50)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");
        }
    }
}
