using Microsoft.EntityFrameworkCore.Migrations;

namespace NZMartialArtsGymFinderAPI.Migrations
{
    public partial class GymOnlyStoresMartialArtsIdsCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MartialArts_Gyms_GymId",
                table: "MartialArts");

            migrationBuilder.DropIndex(
                name: "IX_MartialArts_GymId",
                table: "MartialArts");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "MartialArts");

            migrationBuilder.CreateTable(
                name: "IdCollection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdCollection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdCollection_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdCollection_GymId",
                table: "IdCollection",
                column: "GymId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdCollection");

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "MartialArts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MartialArts_GymId",
                table: "MartialArts",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_MartialArts_Gyms_GymId",
                table: "MartialArts",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
