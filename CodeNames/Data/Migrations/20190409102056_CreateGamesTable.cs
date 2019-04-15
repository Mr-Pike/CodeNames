using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CodeNames.Data.Migrations
{
    public partial class CreateGamesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ScoreATeam = table.Column<short>(defaultValue: 0, nullable:false),
                    ScoreBTeam = table.Column<short>(defaultValue: 0, nullable: false),
                    RoundATeam = table.Column<short>(defaultValue: 0, nullable: false),
                    RoundBTeam = table.Column<short>(defaultValue: 0, nullable: false),
                    StartTeamId = table.Column<short>(nullable: false),
                    NextToPlayTeamId = table.Column<short>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesId", x => x.Id);

                    table.ForeignKey(
                        name: "FK_Games_Teams_StartTeamId",
                        column: x => x.StartTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    table.ForeignKey(
                        name: "FK_Games_Teams_NextToPlayTeamId",
                        column: x => x.NextToPlayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Games");
        }
    }
}
