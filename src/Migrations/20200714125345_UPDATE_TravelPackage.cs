using Microsoft.EntityFrameworkCore.Migrations;

namespace dream_holiday.Migrations
{
    public partial class UPDATE_TravelPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "TravelPackage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "TravelPackage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "TravelPackage");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "TravelPackage");
        }
    }
}
