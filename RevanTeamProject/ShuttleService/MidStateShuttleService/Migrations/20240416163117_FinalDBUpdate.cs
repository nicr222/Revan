using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class FinalDBUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Bus_BusId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CheckIn");

            migrationBuilder.RenameColumn(
                name: "BusId",
                table: "Routes",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_BusId",
                table: "Routes",
                newName: "IX_Routes_DriverId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Routes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Registration",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Message",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Location",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Feedback",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Feedback",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "Anonymous",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Feedback",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DispatchMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CheckIn",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "CheckIn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CheckIn",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_LocationId",
                table: "CheckIn",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Location_LocationId",
                table: "CheckIn",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Driver_DriverId",
                table: "Routes",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Location_LocationId",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Driver_DriverId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_LocationId",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DispatchMessage");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CheckIn");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Routes",
                newName: "BusId");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DriverId",
                table: "Routes",
                newName: "IX_Routes_BusId");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Feedback",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "Feedback",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "Anonymous");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CheckIn",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Bus_BusId",
                table: "Routes",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
