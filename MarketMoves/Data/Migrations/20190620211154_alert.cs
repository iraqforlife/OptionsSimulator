using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class alert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Entry = table.Column<string>(nullable: false),
                    ProfitPrice = table.Column<string>(nullable: false),
                    ProfitTarget = table.Column<string>(nullable: false),
                    LossPrice = table.Column<string>(nullable: false),
                    LossTarget = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Option = table.Column<string>(nullable: false),
                    RiskReward = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Image1Link = table.Column<string>(nullable: true),
                    Image1Description = table.Column<string>(nullable: true),
                    Image2Link = table.Column<string>(nullable: true),
                    Image2escription = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerts");
        }
    }
}
