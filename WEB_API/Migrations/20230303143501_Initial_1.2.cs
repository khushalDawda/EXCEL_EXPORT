using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEB_API.Migrations
{
    public partial class Initial_12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CHEQUE_NO",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Entry_Date",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Extra_1",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Extra_2",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Extra_3",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Extra_4",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PARTICULAR",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Principal",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "SOURCE",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Tdate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CHEQUE_NO",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Debit",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Entry_Date",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Extra_1",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Extra_2",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Extra_3",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Extra_4",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "PARTICULAR",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "SOURCE",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Tdate",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "EXTRA1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EXTRA2",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EXTRA3",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EXTRA4",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Entry_Date",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Aadhar_No",
                table: "Loans",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Report_Date",
                table: "Loans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soc_No",
                table: "Loans",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Society_Name",
                table: "Loans",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Loans",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Aadhar_No",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Report_Date",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soc_No",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Society_Name",
                table: "Deposits",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Deposits",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Aadhar_No",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Report_Date",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soc_No",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Society_Name",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Accounts",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aadhar_No",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Report_Date",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Soc_No",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Society_Name",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Aadhar_No",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Report_Date",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Soc_No",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Society_Name",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Aadhar_No",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Report_Date",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Soc_No",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Society_Name",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "CHEQUE_NO",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Entry_Date",
                table: "Loans",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_1",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_2",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_3",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_4",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Interest",
                table: "Loans",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Other",
                table: "Loans",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PARTICULAR",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Penalty",
                table: "Loans",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Principal",
                table: "Loans",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SOURCE",
                table: "Loans",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tdate",
                table: "Loans",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Loans",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CHEQUE_NO",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Credit",
                table: "Deposits",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Debit",
                table: "Deposits",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Entry_Date",
                table: "Deposits",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_1",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_2",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_3",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extra_4",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PARTICULAR",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SOURCE",
                table: "Deposits",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tdate",
                table: "Deposits",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EXTRA1",
                table: "Accounts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EXTRA2",
                table: "Accounts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EXTRA3",
                table: "Accounts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EXTRA4",
                table: "Accounts",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Entry_Date",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
