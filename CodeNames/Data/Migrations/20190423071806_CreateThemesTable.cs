using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeNames.Data.Migrations
{
    public partial class CreateThemeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                    .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemesId", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ThemeNameIndex",
                table: "Themes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
