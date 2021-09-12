using Microsoft.EntityFrameworkCore.Migrations;

namespace PickEmLeagueDatabase.Migrations
{
    public partial class AddProfileKeyToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureKey",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureKey",
                table: "Users");
        }
    }
}
