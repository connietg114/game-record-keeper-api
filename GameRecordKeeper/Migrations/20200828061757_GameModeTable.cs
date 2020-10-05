using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRecordKeeper.Migrations
{
    public partial class GameModeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameModes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gameID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    WinCondition = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameModes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GameModes_Games_gameID",
                        column: x => x.gameID,
                        principalTable: "Games",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameModes_gameID",
                table: "GameModes",
                column: "gameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameModes");
        }
    }
}
