using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PreFlight.AI.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeJobCategory",
                columns: table => new
                {
                    employeeJobCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobCategory", x => x.employeeJobCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLocation",
                columns: table => new
                {
                    employeeLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeLocationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLocation", x => x.employeeLocationId);
                });

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    JobCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.JobCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "typedUsers",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    RowVersion = table.Column<int>(nullable: false),
                    JoinedDate = table.Column<DateTime>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_typedUsers", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    weatherID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirPressure = table.Column<double>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    WeightValue = table.Column<double>(nullable: false),
                    weatherLink = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.weatherID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    employeeLocationId = table.Column<int>(nullable: false),
                    employeeJobCategoryId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    JoinedDate = table.Column<DateTime>(nullable: true),
                    ExitDate = table.Column<DateTime>(nullable: true),
                    RowVersion = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeJobCategory_employeeJobCategoryId",
                        column: x => x.employeeJobCategoryId,
                        principalTable: "EmployeeJobCategory",
                        principalColumn: "employeeJobCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeLocation_employeeLocationId",
                        column: x => x.employeeLocationId,
                        principalTable: "EmployeeLocation",
                        principalColumn: "employeeLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "BirthDate", "City", "Comment", "Email", "ExitDate", "FirstName", "JoinedDate", "LastName", "Password", "PhoneNumber", "RowVersion", "Street", "Zip", "employeeJobCategoryId", "employeeLocationId" },
                values: new object[] { 1, new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juneau", "Using Fake Address and Phone number here", "danny.dowling@gmail.com", null, "Danny", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dowling", "Password", "324777888773", 0, "1 Grimoire Place", "99801", 9, 4 });

            migrationBuilder.InsertData(
                table: "JobCategories",
                columns: new[] { "JobCategoryId", "JobCategoryName" },
                values: new object[,]
                {
                    { 6, "IT Lead" },
                    { 5, "IT Worker" },
                    { 4, "Worker" },
                    { 3, "Verified" },
                    { 2, "Visitor" },
                    { 1, "Guest" },
                    { 9, "Owner" },
                    { 8, "Senior Manager" },
                    { 7, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 7, "Wrangell" },
                    { 6, "Sitka" },
                    { 5, "Ketchikan" },
                    { 4, "Anchorage" },
                    { 3, "Fairbanks" },
                    { 2, "Seattle" },
                    { 1, "Juneau" },
                    { 8, "Petersburg" }
                });

            migrationBuilder.InsertData(
                table: "Weathers",
                columns: new[] { "weatherID", "AirPressure", "Temperature", "WeightValue", "weatherLink" },
                values: new object[] { 1, 1.0, 60.0, 6.7000000000000002, null });

            migrationBuilder.InsertData(
                table: "typedUsers",
                columns: new[] { "userId", "Comment", "Email", "ExitDate", "FirstName", "JoinedDate", "LastName", "Password", "RowVersion" },
                values: new object[] { 1, "Using Fake Address and Phone number here", "danny.dowling@gmail.com", null, "Danny", new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dowling", "password", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employeeJobCategoryId",
                table: "Employees",
                column: "employeeJobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employeeLocationId",
                table: "Employees",
                column: "employeeLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "typedUsers");

            migrationBuilder.DropTable(
                name: "Weathers");

            migrationBuilder.DropTable(
                name: "EmployeeJobCategory");

            migrationBuilder.DropTable(
                name: "EmployeeLocation");
        }
    }
}
