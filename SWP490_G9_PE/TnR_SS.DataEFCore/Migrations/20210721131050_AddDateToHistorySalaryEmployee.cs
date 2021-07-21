using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddDateToHistorySalaryEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "HistorySalaryEmp");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "HistorySalaryEmp");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "6fe01118-9bab-439c-ad9f-97acae3c3697", new DateTime(2021, 7, 21, 20, 10, 50, 120, DateTimeKind.Local).AddTicks(5240) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "ebe8bbc0-c719-4857-860c-ea0a63fd1814", new DateTime(2021, 7, 21, 20, 10, 50, 120, DateTimeKind.Local).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "7d2b87f3-2655-4645-a48f-e0227cdbb4c6", new DateTime(2021, 7, 21, 20, 10, 50, 105, DateTimeKind.Local).AddTicks(9610) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "HistorySalaryEmp");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "HistorySalaryEmp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "HistorySalaryEmp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "ecf7f7f9-6956-4048-831c-21cc7c278f9f", new DateTime(2021, 7, 21, 0, 25, 42, 970, DateTimeKind.Local).AddTicks(210) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8cd43b6b-9185-4df1-959f-6565565b63a9", new DateTime(2021, 7, 21, 0, 25, 42, 970, DateTimeKind.Local).AddTicks(245) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "3a9819b1-902c-4506-bafe-442eed9d84d8", new DateTime(2021, 7, 21, 0, 25, 42, 968, DateTimeKind.Local).AddTicks(4898) });
        }
    }
}
