using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.Migrations
{
    public partial class initial14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Soc_Id",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soc_Name",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Soc_Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Soc_Name",
                table: "AspNetUsers");
        }
    }
}
