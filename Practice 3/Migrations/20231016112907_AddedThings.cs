using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice_3.Migrations
{
    public partial class AddedThings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfilePhotos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfilePhotos_AspNetUsers_UserId",
                table: "ProfilePhotos");

            migrationBuilder.DropIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "ProfilePhotos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfilePhotos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
