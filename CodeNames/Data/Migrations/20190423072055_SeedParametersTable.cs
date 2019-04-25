using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class SeedParametersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Parameters (Id, Name, Description, Value) VALUES
                (1, 'TimerGiveWord', 'Timer to give the word.', '45'),
                (2, 'TimerGuessWord', 'Timer to guess the word.',  '45'),
                (3, 'BlackModeEnabled', 'Enable black mode.',  'true'),
                (4, 'BlackModeBackgroundColor', 'Background color for the black mode.', '#1e1e1e'),
                (5, 'BlackModeColor', 'Text color for the black mode.', '#d9d9d9'),
                (6, 'WhiteModeBackgroundColor', 'Background color for the white mode.', '#ffffff'),
                (7, 'WhiteModeColor', 'Text color for the white mode.', '#000000')",
                false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE Parameters", true);
        }
    }
}
