using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class SeedTeamsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Teams (Id, Name, Color) VALUES
                (1, 'Blue Team', '#0763cc'),
                (2, 'Red Team', '#cc083f'),
                (3, 'Neutral Team', '#ccbb07'),
                (4, 'Loose Team', '#1a1b1c')", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Teams", true);
        }
    }
}
