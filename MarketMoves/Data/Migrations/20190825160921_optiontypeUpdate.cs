using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class optiontypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OptionType",
                table: "Alerts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionType",
                table: "Alerts");
        }
    }
}
