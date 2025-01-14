using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdToRegisterModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Registration",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedRouteDetail",
                table: "DispatchMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "SelectedRouteDetail",
                table: "DispatchMessage");
        }
    }
}
