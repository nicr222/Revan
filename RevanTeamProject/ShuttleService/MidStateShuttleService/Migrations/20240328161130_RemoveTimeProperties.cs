using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "DropOffTime",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "PickUpTime",
                table: "Registration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "Registration",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Registration",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DropOffTime",
                table: "Registration",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PickUpTime",
                table: "Registration",
                type: "time",
                nullable: true);
        }
    }
}
