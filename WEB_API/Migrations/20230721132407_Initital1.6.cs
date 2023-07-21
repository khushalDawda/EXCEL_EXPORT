using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.Migrations
{
    public partial class Initital16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuMaster",
                columns: table => new
                {
                    MenuIdentity = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuID = table.Column<string>(nullable: true),
                    MenuName = table.Column<string>(nullable: true),
                    Parent_MenuID = table.Column<string>(nullable: true),
                    User_Roll = table.Column<string>(nullable: true),
                    MenuFileName = table.Column<string>(nullable: true),
                    MenuURL = table.Column<string>(nullable: true),
                    USE_YN = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMaster", x => x.MenuIdentity);
                });

            migrationBuilder.CreateTable(
                name: "UserWiseMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    MenuID = table.Column<string>(nullable: true),
                    Add = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    USE_YN = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWiseMenu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuMaster");

            migrationBuilder.DropTable(
                name: "UserWiseMenu");
        }
    }
}
