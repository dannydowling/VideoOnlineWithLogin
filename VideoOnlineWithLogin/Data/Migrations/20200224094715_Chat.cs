using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoOnlineWithLogin.Data.Migrations
{
    public partial class Chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "typedUsers",
                columns: new[] { "userId", "Comment", "Email", "ExitDate", "FirstName", "JoinedDate", "LastName", "Password" },
                values: new object[] { 1, "Using Fake Address and Phone number here", "danny.dowling@gmail.com", null, "Danny", new DateTime(2019, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dowling", "password" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "typedUsers",
                keyColumn: "userId",
                keyValue: 1);
        }
    }
}
