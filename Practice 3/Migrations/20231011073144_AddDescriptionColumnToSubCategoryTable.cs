using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice_3.Migrations
{
    public partial class AddDescriptionColumnToSubCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubCategories");
        }
    }
}
