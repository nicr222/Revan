using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class removeCheckInUserFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_User_UserId",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_UserId",
                table: "CheckIn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
