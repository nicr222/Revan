using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class BusNumToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CheckIn",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BusNo",
                table: "Bus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_UserId",
                table: "CheckIn",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_User_UserId",
                table: "CheckIn",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_User_UserId",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_UserId",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CheckIn");

            migrationBuilder.AlterColumn<string>(
                name: "BusNo",
                table: "Bus",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
