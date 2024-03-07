using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MidStateShuttleService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumb = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Driver__F1B1CD049A2B343A", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false, defaultValue: "WI"),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__E7FEA477A8B727BF", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4C2781EAC2", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__6A4BEDF69EC0AC18", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "UserId",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CurrentRouteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bus__6A0F60B5718116B1", x => x.BusId);
                    table.ForeignKey(
                        name: "FK__Bus__DriverID__4D5F7D71",
                        column: x => x.DriverID,
                        principalTable: "Driver",
                        principalColumn: "DriverId");
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickUpLocationID = table.Column<int>(type: "int", nullable: false),
                    DropOffLocationID = table.Column<int>(type: "int", nullable: false),
                    PickUpTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DropOffTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Route__80979AAD3C88294B", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK__Route__BusId__55009F39",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                    table.ForeignKey(
                        name: "FK__Route__DropOffLo__19DFD96B",
                        column: x => x.DropOffLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK__Route__PickUpLoc__18EBB532",
                        column: x => x.PickUpLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    ServiceMessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__02DEBA9EF8FCEC20", x => x.ServiceMessageID);
                    table.ForeignKey(
                        name: "FK__Message__DriverI__1DB06A4F",
                        column: x => x.DriverID,
                        principalTable: "Driver",
                        principalColumn: "DriverId");
                    table.ForeignKey(
                        name: "FK__Message__RouteID__1CBC4616",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "RouteID");
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PreferContact = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AgreeToTerms = table.Column<bool>(type: "bit", nullable: false),
                    SpecialRequestDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SepcialRequest = table.Column<bool>(type: "bit", nullable: false),
                    CheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    FeedBackId = table.Column<int>(type: "int", nullable: true),
                    TripType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstDayExpectingToRide = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Registra__6EF588103B2104F6", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK__Registrat__FeedB__2739D489",
                        column: x => x.FeedBackId,
                        principalTable: "Feedback",
                        principalColumn: "FeedbackID");
                    table.ForeignKey(
                        name: "FK__Registrat__Route__2645B050",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteID");
                });

            migrationBuilder.CreateTable(
                name: "RouteLocation",
                columns: table => new
                {
                    RouteLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    NextStopID = table.Column<int>(type: "int", nullable: false),
                    ArrivalTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DepartureTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsStartLocation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RouteLoc__ADC74A46FA981E29", x => x.RouteLocationID);
                    table.ForeignKey(
                        name: "FK__RouteLoca__Locat__1EA48E88",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK__RouteLoca__NextS__1F98B2C1",
                        column: x => x.NextStopID,
                        principalTable: "Location",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK__RouteLoca__Route__57DD0BE4",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "RouteID");
                });

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
                    table.PrimaryKey("PK__Registra__F8B74C8E0554913D", x => x.RegistrationDayID);
                    table.ForeignKey(
                        name: "FK__Registrat__Regis__5AB9788F",
                        column: x => x.RegistrationID,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_CurrentRouteId",
                table: "Bus",
                column: "CurrentRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverID",
                table: "Bus",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserID",
                table: "Feedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_DriverID",
                table: "Message",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RouteID",
                table: "Message",
                column: "RouteID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_FeedBackId",
                table: "Registration",
                column: "FeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_RouteId",
                table: "Registration",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationDays_RegistrationID",
                table: "RegistrationDays",
                column: "RegistrationID");

            migrationBuilder.CreateIndex(
                name: "IX_Route_BusId",
                table: "Route",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_DropOffLocationID",
                table: "Route",
                column: "DropOffLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Route_PickUpLocationID",
                table: "Route",
                column: "PickUpLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocation_LocationID",
                table: "RouteLocation",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocation_NextStopID",
                table: "RouteLocation",
                column: "NextStopID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocation_RouteID",
                table: "RouteLocation",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK__Bus__CurrentRout__4E53A1AA",
                table: "Bus",
                column: "CurrentRouteId",
                principalTable: "Route",
                principalColumn: "RouteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Bus__CurrentRout__4E53A1AA",
                table: "Bus");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "RegistrationDays");

            migrationBuilder.DropTable(
                name: "RouteLocation");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Driver");
        }
    }
}
