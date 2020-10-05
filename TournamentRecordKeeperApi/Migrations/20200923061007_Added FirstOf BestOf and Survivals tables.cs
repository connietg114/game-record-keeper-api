using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRecordKeeper.Migrations
{
    public partial class AddedFirstOfBestOfandSurvivalstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BestOf",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameModeID = table.Column<int>(nullable: true),
                    Win = table.Column<int>(nullable: false),
                    NumberOfMatches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestOf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BestOf_GameModes_gameModeID",
                        column: x => x.gameModeID,
                        principalTable: "GameModes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FirstOf",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameModeID = table.Column<int>(nullable: true),
                    Threshold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstOf", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FirstOf_GameModes_gameModeID",
                        column: x => x.gameModeID,
                        principalTable: "GameModes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Survivals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameModeID = table.Column<int>(nullable: true),
                    Threshold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survivals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Survivals_GameModes_gameModeID",
                        column: x => x.gameModeID,
                        principalTable: "GameModes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestOf_gameModeID",
                table: "BestOf",
                column: "gameModeID");

            migrationBuilder.CreateIndex(
                name: "IX_FirstOf_gameModeID",
                table: "FirstOf",
                column: "gameModeID");

            migrationBuilder.CreateIndex(
                name: "IX_Survivals_gameModeID",
                table: "Survivals",
                column: "gameModeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestOf");

            migrationBuilder.DropTable(
                name: "FirstOf");

            migrationBuilder.DropTable(
                name: "Survivals");
        }
    }
}
