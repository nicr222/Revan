using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class AddDateAndIpFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceIpAddress",
                table: "Registration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EditDateTime",
                table: "Registration",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDateTime",
                table: "Registration",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceIpAddress",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "EditDateTime",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "InsertDateTime",
                table: "Registration");
        }
    }
}
