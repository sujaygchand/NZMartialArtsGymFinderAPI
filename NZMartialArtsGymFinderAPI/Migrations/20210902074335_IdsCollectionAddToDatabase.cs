using Microsoft.EntityFrameworkCore.Migrations;

namespace NZMartialArtsGymFinderAPI.Migrations
{
    public partial class IdsCollectionAddToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdCollection_Gyms_GymId",
                table: "IdCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdCollection",
                table: "IdCollection");

            migrationBuilder.RenameTable(
                name: "IdCollection",
                newName: "IdCollections");

            migrationBuilder.RenameIndex(
                name: "IX_IdCollection_GymId",
                table: "IdCollections",
                newName: "IX_IdCollections_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdCollections",
                table: "IdCollections",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdCollections",
                table: "IdCollections");

            migrationBuilder.RenameTable(
                name: "IdCollections",
                newName: "IdCollection");

            migrationBuilder.RenameIndex(
                name: "IX_IdCollections_GymId",
                table: "IdCollection",
                newName: "IX_IdCollection_GymId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdCollection",
                table: "IdCollection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdCollection_Gyms_GymId",
                table: "IdCollection",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
