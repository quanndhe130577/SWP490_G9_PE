using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class AddBonousPunishToHistorySalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Bonus",
                table: "HistorySalaryEmp",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Punish",
                table: "HistorySalaryEmp",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "614a184f-6ae8-488b-82bc-f45357101ff9", new DateTime(2021, 8, 2, 23, 15, 45, 381, DateTimeKind.Local).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9a6c19e2-f9e6-4967-aaaa-c66edb32734c", new DateTime(2021, 8, 2, 23, 15, 45, 381, DateTimeKind.Local).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "edec0cfe-45c7-4280-b91b-9e3f08a09ac3", new DateTime(2021, 8, 2, 23, 15, 45, 358, DateTimeKind.Local).AddTicks(7330) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "HistorySalaryEmp");

            migrationBuilder.DropColumn(
                name: "Punish",
                table: "HistorySalaryEmp");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f063380b-db97-4ac3-9765-0233e625c57a", new DateTime(2021, 8, 1, 11, 5, 25, 940, DateTimeKind.Local).AddTicks(802) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c10b2495-75b5-47c1-a107-f5d790346131", new DateTime(2021, 8, 1, 11, 5, 25, 940, DateTimeKind.Local).AddTicks(851) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "385c230e-4515-438e-8bc4-b4f22080b2e0", new DateTime(2021, 8, 1, 11, 5, 25, 937, DateTimeKind.Local).AddTicks(8551) });
        }
    }
}
