using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FootballCatalog30.Api.Migrations
{
    /// <inheritdoc />
    public partial class SetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Зенит" },
                    { 2, "Локомотив" },
                    { 3, "Даллас" },
                    { 4, "Милан" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BirthDate", "CommandId", "CountryId", "Name", "Sex", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1997, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Александр", 0, "Соболев" },
                    { 2, new DateTime(1987, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "Михаил", 0, "Кержаков" },
                    { 3, new DateTime(1997, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "Илья", 0, "Самошников" },
                    { 4, new DateTime(2000, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "Хесус", 0, "Феррейра" },
                    { 5, new DateTime(1992, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, "Альваро", 0, "Мората" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
