using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class accountUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Suscribed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuscriptionExpiration",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suscribed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SuscriptionExpiration",
                table: "AspNetUsers");
        }
    }
}
