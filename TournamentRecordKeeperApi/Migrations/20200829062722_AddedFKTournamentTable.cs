using Microsoft.EntityFrameworkCore.Migrations;

namespace GameRecordKeeper.Migrations
{
    public partial class AddedFKTournamentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tournamentTypes",
                table: "tournamentTypes");

            migrationBuilder.DropColumn(
                name: "TournamentType",
                table: "Tournaments");

            migrationBuilder.RenameTable(
                name: "tournamentTypes",
                newName: "TournamentTypes");

            migrationBuilder.AddColumn<int>(
                name: "tournamentTypeID",
                table: "Tournaments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TournamentTypes",
                table: "TournamentTypes",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_tournamentTypeID",
                table: "Tournaments",
                column: "tournamentTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_TournamentTypes_tournamentTypeID",
                table: "Tournaments",
                column: "tournamentTypeID",
                principalTable: "TournamentTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_TournamentTypes_tournamentTypeID",
                table: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TournamentTypes",
                table: "TournamentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_tournamentTypeID",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "tournamentTypeID",
                table: "Tournaments");

            migrationBuilder.RenameTable(
                name: "TournamentTypes",
                newName: "tournamentTypes");

            migrationBuilder.AddColumn<int>(
                name: "TournamentType",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tournamentTypes",
                table: "tournamentTypes",
                column: "ID");
        }
    }
}
