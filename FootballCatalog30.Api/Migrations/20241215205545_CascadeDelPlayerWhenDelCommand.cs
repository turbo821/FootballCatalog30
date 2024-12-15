using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballCatalog30.Api.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelPlayerWhenDelCommand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Commands_CommandId",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Commands_CommandId",
                table: "Players",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Commands_CommandId",
                table: "Players");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Commands_CommandId",
                table: "Players",
                column: "CommandId",
                principalTable: "Commands",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
