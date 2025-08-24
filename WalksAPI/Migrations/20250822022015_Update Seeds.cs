using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Walks.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("05b87b94-b740-431c-9f80-1ebd82ada49e"), "Easy" },
                    { new Guid("832e0765-0f4a-43a0-b098-7349f0ba2c01"), "Hard" },
                    { new Guid("95ef9256-0d28-4bac-ad4d-b2c15267cd5d"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("02a69f01-994f-4ac6-b293-bb14ff574f81"), "TOR", "Toronto", "https://upload.wikimedia.org/wikipedia/commons/a/a1/Hillside_Gardens_%2824677009128%29.jpg" },
                    { new Guid("45cb6417-c2b3-4937-9916-11341a49a08a"), "MIS", "Mississauga", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("05b87b94-b740-431c-9f80-1ebd82ada49e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("832e0765-0f4a-43a0-b098-7349f0ba2c01"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("95ef9256-0d28-4bac-ad4d-b2c15267cd5d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("02a69f01-994f-4ac6-b293-bb14ff574f81"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("45cb6417-c2b3-4937-9916-11341a49a08a"));

        
        }
    }
}
