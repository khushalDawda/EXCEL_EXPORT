using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.Migrations
{
    public partial class initial_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Loans",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Deposits",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Loans",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Deposits",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Time",
                table: "Accounts",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
