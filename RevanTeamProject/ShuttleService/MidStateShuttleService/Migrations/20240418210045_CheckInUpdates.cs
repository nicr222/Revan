using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class CheckInUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Bus_BusId",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Routes_RouteId",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_BusId",
                table: "CheckIn");

            migrationBuilder.DropIndex(
                name: "IX_CheckIn_RouteId",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "BusNumber",
                table: "CheckIn");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "CheckIn");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CheckIn",
                type: "datetime",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Bus",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CheckIn",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "CheckIn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusNumber",
                table: "CheckIn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "CheckIn",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Bus",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_BusId",
                table: "CheckIn",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_RouteId",
                table: "CheckIn",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Bus_BusId",
                table: "CheckIn",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Routes_RouteId",
                table: "CheckIn",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteID");
        }
    }
}
