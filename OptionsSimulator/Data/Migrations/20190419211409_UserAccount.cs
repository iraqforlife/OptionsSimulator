using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionsSimulator.Data.Migrations
{
    public partial class UserAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Balance",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cash",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Equity",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitialBalance",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Open = table.Column<DateTime>(nullable: false),
                    Close = table.Column<DateTime>(nullable: false),
                    Entry = table.Column<double>(nullable: false),
                    Exit = table.Column<double>(nullable: false),
                    Gains = table.Column<double>(nullable: false),
                    Symbol = table.Column<string>(nullable: false),
                    NumberOfContracts = table.Column<int>(nullable: false),
                    StrikePrice = table.Column<double>(nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Equity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InitialBalance",
                table: "AspNetUsers");
        }
    }
}
