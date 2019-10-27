using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketMoves.Data.Migrations
{
    public partial class competitiontitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Play_AspNetUsers_AccountId",
                table: "Play");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Play",
                table: "Play");

            migrationBuilder.RenameTable(
                name: "Play",
                newName: "Plays");

            migrationBuilder.RenameIndex(
                name: "IX_Play_AccountId",
                table: "Plays",
                newName: "IX_Plays_AccountId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Plays",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plays",
                table: "Plays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plays_AspNetUsers_AccountId",
                table: "Plays",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plays_AspNetUsers_AccountId",
                table: "Plays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plays",
                table: "Plays");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Plays");

            migrationBuilder.RenameTable(
                name: "Plays",
                newName: "Play");

            migrationBuilder.RenameIndex(
                name: "IX_Plays_AccountId",
                table: "Play",
                newName: "IX_Play_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Play",
                table: "Play",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Play_AspNetUsers_AccountId",
                table: "Play",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
