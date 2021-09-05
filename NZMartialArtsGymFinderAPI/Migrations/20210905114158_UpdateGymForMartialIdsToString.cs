using Microsoft.EntityFrameworkCore.Migrations;

namespace NZMartialArtsGymFinderAPI.Migrations
{
    public partial class UpdateGymForMartialIdsToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MartialArtsKey");

            migrationBuilder.AddColumn<string>(
                name: "MartialArtIds",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MartialArtIds",
                table: "Gyms");

            migrationBuilder.CreateTable(
                name: "MartialArtsKey",
                columns: table => new
                {
                    MartialArtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MartialArtsKey", x => x.MartialArtId);
                    table.ForeignKey(
                        name: "FK_MartialArtsKey_Gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "Gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MartialArtsKey_GymId",
                table: "MartialArtsKey",
                column: "GymId");
        }
    }
}
