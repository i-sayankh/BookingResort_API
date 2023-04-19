using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingResort_ResortAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToResortTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResortId",
                table: "ResortNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResortNumbers_ResortId",
                table: "ResortNumbers",
                column: "ResortId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResortNumbers_Resorts_ResortId",
                table: "ResortNumbers",
                column: "ResortId",
                principalTable: "Resorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResortNumbers_Resorts_ResortId",
                table: "ResortNumbers");

            migrationBuilder.DropIndex(
                name: "IX_ResortNumbers_ResortId",
                table: "ResortNumbers");

            migrationBuilder.DropColumn(
                name: "ResortId",
                table: "ResortNumbers");
        }
    }
}
