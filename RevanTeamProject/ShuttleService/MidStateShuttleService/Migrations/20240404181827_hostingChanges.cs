using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class hostingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Route",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Routes_RouteID1",
                table: "Registration");

            migrationBuilder.DropTable(
                name: "RegistrationDays");

            migrationBuilder.DropIndex(
                name: "IX_Registration_RouteId",
                table: "Registration");

            migrationBuilder.DropIndex(
                name: "IX_Registration_RouteID1",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RouteID",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RouteID1",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "DispatchMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteID",
                table: "Registration",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteID1",
                table: "Registration",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "DispatchMessage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RegistrationDays",
                columns: table => new
                {
                    RegistrationDayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationID = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationDays", x => x.RegistrationDayID);
                    table.ForeignKey(
                        name: "FK_RegistertionDaysModel_Registration",
                        column: x => x.RegistrationID,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_RouteId",
                table: "Registration",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_RouteID1",
                table: "Registration",
                column: "RouteID1");

            migrationBuilder.CreateIndex(
                name: "IX_RegistertionDaysModel_RegistrationID",
                table: "RegistrationDays",
                column: "RegistrationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Route",
                table: "Registration",
                column: "RouteID",
                principalTable: "Routes",
                principalColumn: "RouteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Routes_RouteID1",
                table: "Registration",
                column: "RouteID1",
                principalTable: "Routes",
                principalColumn: "RouteID");
        }
    }
}
