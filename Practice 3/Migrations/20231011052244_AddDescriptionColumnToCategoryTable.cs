using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice_3.Migrations
{
    public partial class AddDescriptionColumnToCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desctiption",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desctiption",
                table: "Categories");
        }
    }
}
