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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesId", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Games");
        }
    }
}
