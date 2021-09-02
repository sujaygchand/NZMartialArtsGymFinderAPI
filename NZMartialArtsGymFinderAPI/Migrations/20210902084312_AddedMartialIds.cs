using Microsoft.EntityFrameworkCore.Migrations;

namespace NZMartialArtsGymFinderAPI.Migrations
{
    public partial class AddedMartialIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "IdCollections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdCollections_GymId",
                table: "IdCollections",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdCollections_Gyms_GymId",
                table: "IdCollections",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdCollections_Gyms_GymId",
                table: "IdCollections");

            migrationBuilder.DropIndex(
                name: "IX_IdCollections_GymId",
                table: "IdCollections");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "IdCollections");
        }
    }
}
