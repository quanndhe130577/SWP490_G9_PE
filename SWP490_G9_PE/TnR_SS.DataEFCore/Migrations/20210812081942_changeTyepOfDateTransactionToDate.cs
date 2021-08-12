using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class changeTyepOfDateTransactionToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "1b27f43d-8e28-43be-a0c2-e2648237fd0d", new DateTime(2021, 8, 12, 15, 19, 41, 372, DateTimeKind.Local).AddTicks(9382) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f203f6ca-9a62-49cf-b9e8-0c1b3b60c0d9", new DateTime(2021, 8, 12, 15, 19, 41, 372, DateTimeKind.Local).AddTicks(9423) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "de869db4-d0d2-493c-a666-01a85abe550e", new DateTime(2021, 8, 12, 15, 19, 41, 370, DateTimeKind.Local).AddTicks(6146) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transaction",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "10035ac6-3f22-4ffe-9b4b-0d71246bf11d", new DateTime(2021, 8, 10, 16, 34, 54, 9, DateTimeKind.Local).AddTicks(3467) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "32dc83bc-5503-4dca-9cb1-76b6537ef0f8", new DateTime(2021, 8, 10, 16, 34, 54, 9, DateTimeKind.Local).AddTicks(3509) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c9a027f3-f573-4ca5-8a36-e084943d337a", new DateTime(2021, 8, 10, 16, 34, 54, 6, DateTimeKind.Local).AddTicks(9079) });
        }
    }
}
