using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class CreateWordsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW _WordsView
                                    AS 
                                    SELECT W.id, W.Name, REPLACE(GROUP_CONCAT(T.Name), ',', ', ') AS ThemesName 
                                    FROM Words W
                                    LEFT JOIN ThemesWords TW ON W.id = TW.WordId
                                    LEFT JOIN Themes T ON T.id = TW.ThemeId
                                    GROUP BY W.id ", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS _WordsView", true);
        }
    }
}
