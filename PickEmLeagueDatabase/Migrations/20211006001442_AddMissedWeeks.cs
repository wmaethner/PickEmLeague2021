using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PickEmLeagueDatabase.Migrations
{
    public partial class AddMissedWeeks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<int>>(
                name: "MissedWeeks",
                table: "Users",
                type: "integer[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissedWeeks",
                table: "Users");
        }
    }
}
