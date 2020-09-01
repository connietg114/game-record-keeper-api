using Microsoft.EntityFrameworkCore.Migrations;

namespace TournamentRecordKeeperApi.Migrations
{
    public partial class AddedWinConditionAndAddedFKinGameMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinCondition",
                table: "GameModes");

            migrationBuilder.AddColumn<int>(
                name: "winConditionID",
                table: "GameModes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "tournamentID",
                table: "GameMatches",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WinConditions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinConditions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameModes_winConditionID",
                table: "GameModes",
                column: "winConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_GameMatches_tournamentID",
                table: "GameMatches",
                column: "tournamentID");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMatches_Tournaments_tournamentID",
                table: "GameMatches",
                column: "tournamentID",
                principalTable: "Tournaments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameModes_WinConditions_winConditionID",
                table: "GameModes",
                column: "winConditionID",
                principalTable: "WinConditions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMatches_Tournaments_tournamentID",
                table: "GameMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_GameModes_WinConditions_winConditionID",
                table: "GameModes");

            migrationBuilder.DropTable(
                name: "WinConditions");

            migrationBuilder.DropIndex(
                name: "IX_GameModes_winConditionID",
                table: "GameModes");

            migrationBuilder.DropIndex(
                name: "IX_GameMatches_tournamentID",
                table: "GameMatches");

            migrationBuilder.DropColumn(
                name: "winConditionID",
                table: "GameModes");

            migrationBuilder.DropColumn(
                name: "tournamentID",
                table: "GameMatches");

            migrationBuilder.AddColumn<int>(
                name: "WinCondition",
                table: "GameModes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
