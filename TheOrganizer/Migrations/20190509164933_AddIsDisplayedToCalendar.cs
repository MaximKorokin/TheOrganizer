using Microsoft.EntityFrameworkCore.Migrations;

namespace TheOrganizer.Migrations
{
    public partial class AddIsDisplayedToCalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisplayed",
                table: "Calendars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisplayed",
                table: "Calendars");
        }
    }
}
