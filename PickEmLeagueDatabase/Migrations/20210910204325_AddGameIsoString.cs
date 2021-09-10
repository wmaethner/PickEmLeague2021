using Microsoft.EntityFrameworkCore.Migrations;

namespace PickEmLeagueDatabase.Migrations
{
    public partial class AddGameIsoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameIsoString",
                table: "Games",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameIsoString",
                table: "Games");
        }
    }
}
