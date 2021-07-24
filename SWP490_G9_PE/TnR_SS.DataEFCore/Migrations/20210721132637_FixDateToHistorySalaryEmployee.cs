using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class FixDateToHistorySalaryEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "HistorySalaryEmp",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "371e5193-a7f3-4d98-b665-9b915cdfd986", new DateTime(2021, 7, 21, 20, 26, 36, 926, DateTimeKind.Local).AddTicks(9780) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "91431f4f-d06b-4d38-b26d-3e1133cf1d4f", new DateTime(2021, 7, 21, 20, 26, 36, 926, DateTimeKind.Local).AddTicks(9830) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c1e6bb96-9878-4646-a88d-87d0dbc0d639", new DateTime(2021, 7, 21, 20, 26, 36, 913, DateTimeKind.Local).AddTicks(2250) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "HistorySalaryEmp");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "HistorySalaryEmp",
                newName: "Date");

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
    }
}
