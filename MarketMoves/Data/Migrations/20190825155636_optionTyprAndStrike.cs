using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class optionTyprAndStrike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Strike",
                table: "Alerts",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Strike",
                table: "Alerts");
        }
    }
}
