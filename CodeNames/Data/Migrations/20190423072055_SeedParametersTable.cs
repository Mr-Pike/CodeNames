using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class SeedParametersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Parameters (Id, Name, Value) VALUES
                (1, 'Timer', '45'),
                (2, 'BlackModeEnabled', 'true'),
                (3, 'BlackModeBackgroundColor', '#1e1e1e'),
                (4, 'BlackModeColor', '#d9d9d9'),
                (5, 'WhiteModeBackgroundColor', '#ffffff'),
                (6, 'WhiteModeColor', '#000000')",
                false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Parameters", true);
        }
    }
}
