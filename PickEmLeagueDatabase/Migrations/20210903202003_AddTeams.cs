using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PickEmLeagueDatabase.Migrations
{
    public partial class AddTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeam",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeam",
                table: "Games");

            migrationBuilder.AddColumn<long>(
                name: "AwayTeamId",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "HomeTeamId",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "Name" },
                values: new object[,]
                {
                    { 1L, "Arizona", "Cardinals" },
                    { 30L, "Tampa Bay", "Buccaneers" },
                    { 29L, "Seattle", "Seahawks" },
                    { 28L, "San Francisco", "49ers" },
                    { 27L, "Pittsburgh", "Steelers" },
                    { 26L, "Philadelphia", "Eagles" },
                    { 25L, "New York", "Jets" },
                    { 24L, "New York", "Giants" },
                    { 23L, "New Orleans", "Saints" },
                    { 22L, "New England", "Patriots" },
                    { 21L, "Minnesota", "Vikings" },
                    { 20L, "Miami", "Dolphins" },
                    { 19L, "Los Angeles", "Rams" },
                    { 18L, "Los Angeles", "Chargers" },
                    { 17L, "Las Vegas", "Raiders" },
                    { 16L, "Kansas City", "Chiefs" },
                    { 15L, "Jacksonville", "Jaguars" },
                    { 14L, "Indianapolis", "Colts" },
                    { 13L, "Houston", "Texans" },
                    { 12L, "Green Bay", "Packers" },
                    { 11L, "Detroit", "Lions" },
                    { 10L, "Denver", "Broncos" },
                    { 9L, "Dallas", "Cowboys" },
                    { 8L, "Cleveland", "Browns" },
                    { 7L, "Cincinnati", "Bengals" },
                    { 6L, "Chicago", "Bears" },
                    { 5L, "Carolina", "Panthers" },
                    { 4L, "Buffalo", "Bills" },
                    { 3L, "Baltimore", "Ravens" },
                    { 2L, "Atlanta", "Falcons" },
                    { 31L, "Tennessee", "Titans" },
                    { 32L, "Washington", "Football Team" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games",
                column: "AwayTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_AwayTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "AwayTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeam",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeam",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
