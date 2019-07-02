using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class tradingViewUsername2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TradingViewUserName",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradingViewUserName",
                table: "AspNetUsers");

        }
    }
}
