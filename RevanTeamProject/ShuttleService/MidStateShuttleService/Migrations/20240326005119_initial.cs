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
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    message = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    responseRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    contactInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusNo = table.Column<int>(type: "int", nullable: false),
                    PassengerCapacity = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Bus_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DispatchMessage",
                columns: table => new
                {
                    DispatchMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    PickUpLocationID = table.Column<int>(type: "int", nullable: false),
                    DropOffLocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispatchMessage", x => x.DispatchMessageId);
                    table.ForeignKey(
                        name: "FK_DispatchMessage_Location_DropOffLocationID",
                        column: x => x.DropOffLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DispatchMessage_Location_PickUpLocationID",
                        column: x => x.PickUpLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickUpLocationID = table.Column<int>(type: "int", nullable: false),
                    DropOffLocationID = table.Column<int>(type: "int", nullable: false),
                    PickUpTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DropOffTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_Route_Bus_BusId",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Location_DropOffLocationID",
                        column: x => x.DropOffLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Route_Location_PickUpLocationID",
                        column: x => x.PickUpLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    CheckInId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    BusNumber = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstTime = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.CheckInId);
                    table.ForeignKey(
                        name: "FK_CheckIn_Bus_BusId",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckIn_Route_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Route",
                        principalColumn: "RouteID");
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TripType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PickUpLocationID = table.Column<int>(type: "int", nullable: true),
                    DropOffLocationID = table.Column<int>(type: "int", nullable: true),
                    NeedTransportation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PickUpTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DropOffTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    SpecialRequest = table.Column<bool>(type: "bit", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    WhichFriday = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FridayTripType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContactPreference = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AgreeTerms = table.Column<bool>(type: "bit", nullable: false),
                    FridayAgreeTerms = table.Column<bool>(type: "bit", nullable: false),
                    SelectedRouteDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnSelectedRouteDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedDaysOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDayExpectingToRide = table.Column<DateOnly>(type: "date", nullable: true),
                    MustArriveTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    CanLeaveTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    FridayMustArriveTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    FridayCanLeaveTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    SpecialPickUpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialDropOffLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteID1 = table.Column<int>(type: "int", nullable: true),
                    FridayPickUpLocationID = table.Column<int>(type: "int", nullable: true),
                    FridayDropOffLocationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registration_DropOffLocation",
                        column: x => x.DropOffLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_FridayDropOffLocation",
                        column: x => x.FridayDropOffLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_FridayPickUpLocation",
                        column: x => x.FridayPickUpLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_PickUpLocation",
                        column: x => x.PickUpLocationID,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_Route",
                        column: x => x.RouteID,
                        principalTable: "Route",
                        principalColumn: "RouteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_Route_RouteID1",
                        column: x => x.RouteID1,
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
                    table.PrimaryKey("PK_RegistrationDays", x => x.RegistrationDayID);
                    table.ForeignKey(
                        name: "FK_RegistertionDaysModel_Registration",
                        column: x => x.RegistrationID,
                        principalTable: "Registration",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bus_DriverId",
                table: "Bus",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_BusId",
                table: "CheckIn",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_RouteId",
                table: "CheckIn",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchMessage_DropOffLocationID",
                table: "DispatchMessage",
                column: "DropOffLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_DispatchMessage_PickUpLocationID",
                table: "DispatchMessage",
                column: "PickUpLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_UserID",
                table: "Feedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_DropOffLocationID",
                table: "Registration",
                column: "DropOffLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_FridayDropOffLocationID",
                table: "Registration",
                column: "FridayDropOffLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_FridayPickUpLocationID",
                table: "Registration",
                column: "FridayPickUpLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_PickUpLocationID",
                table: "Registration",
                column: "PickUpLocationID");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropTable(
                name: "DispatchMessage");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "RegistrationDays");

            migrationBuilder.DropTable(
                name: "Registration");

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
