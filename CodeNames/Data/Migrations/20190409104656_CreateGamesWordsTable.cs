using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class CreateGamesWordsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamesWords",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    WordId = table.Column<int>(nullable: false),
                    TeamId = table.Column<short>(nullable: false),
                    Order = table.Column<short>(nullable: false),
                    Find = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_GamesWords_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_GamesWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_GamesWords_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.PrimaryKey("PK_WordsGameId", x => new { x.GameId, x.WordId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameWords");
        }
    }
}
