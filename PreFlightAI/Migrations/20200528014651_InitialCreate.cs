using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PreFlight.AI.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "Juneau" },
                    { 2, "Seattle" },
                    { 3, "Fairbanks" },
                    { 4, "Anchorage" },
                    { 5, "Ketchikan" },
                    { 6, "Sitka" },
                    { 7, "Wrangell" },
                    { 8, "Petersburg" }
                });

            migrationBuilder.InsertData(
                table: "Weathers",
                columns: new[] { "weatherID", "AirPressure", "Temperature", "WeightValue", "weatherLink" },
                values: new object[] { 1, 1.0, 60.0, 6.7000000000000002, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
