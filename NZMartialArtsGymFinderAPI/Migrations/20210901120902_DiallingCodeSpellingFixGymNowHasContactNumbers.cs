using Microsoft.EntityFrameworkCore.Migrations;

namespace NZMartialArtsGymFinderAPI.Migrations
{
    public partial class DiallingCodeSpellingFixGymNowHasContactNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DialingCode",
                table: "Regions",
                newName: "DiallingCode");

            migrationBuilder.AddColumn<string>(
                name: "LandlineNumber",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandlineNumber",
                table: "Gyms");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Gyms");

            migrationBuilder.RenameColumn(
                name: "DiallingCode",
                table: "Regions",
                newName: "DialingCode");
        }
    }
}
