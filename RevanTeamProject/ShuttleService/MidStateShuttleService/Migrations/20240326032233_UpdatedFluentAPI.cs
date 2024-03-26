using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Route_RouteId",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Route_RouteID1",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Bus_BusId",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Location_DropOffLocationID",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Location_PickUpLocationID",
                table: "Route");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameIndex(
                name: "IX_Route_BusId",
                table: "Routes",
                newName: "IX_Routes_BusId");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Location",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "WI",
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PickUpTime",
                table: "Routes",
                type: "TIME",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DropOffTime",
                table: "Routes",
                type: "TIME",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalDetails",
                table: "Routes",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Routes_RouteId",
                table: "CheckIn",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Routes_RouteID1",
                table: "Registration",
                column: "RouteID1",
                principalTable: "Routes",
                principalColumn: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Bus_BusId",
                table: "Routes",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Location_DropOffLocationID",
                table: "Routes",
                column: "DropOffLocationID",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Location_PickUpLocationID",
                table: "Routes",
                column: "PickUpLocationID",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Routes_RouteId",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Routes_RouteID1",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Bus_BusId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Location_DropOffLocationID",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Location_PickUpLocationID",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_BusId",
                table: "Route",
                newName: "IX_Route_BusId");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Location",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldDefaultValue: "WI");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PickUpTime",
                table: "Route",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "DropOffTime",
                table: "Route",
                type: "time",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "TIME");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalDetails",
                table: "Route",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Route_RouteId",
                table: "CheckIn",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Route_RouteID1",
                table: "Registration",
                column: "RouteID1",
                principalTable: "Route",
                principalColumn: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Bus_BusId",
                table: "Route",
                column: "BusId",
                principalTable: "Bus",
                principalColumn: "BusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Location_DropOffLocationID",
                table: "Route",
                column: "DropOffLocationID",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Location_PickUpLocationID",
                table: "Route",
                column: "PickUpLocationID",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
