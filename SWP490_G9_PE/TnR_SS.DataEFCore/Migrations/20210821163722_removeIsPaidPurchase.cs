using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class removeIsPaidPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPaid",
                table: "Purchase");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "7f7b03f0-43b3-4bbf-9618-ae000a3042ee", new DateTime(2021, 8, 21, 23, 37, 21, 311, DateTimeKind.Local).AddTicks(2210) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a9c80ad7-0408-4717-ac29-f2368d5536c7", new DateTime(2021, 8, 21, 23, 37, 21, 311, DateTimeKind.Local).AddTicks(2255) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8dd75750-242a-46fd-8fbf-b6a5e695c4d0", new DateTime(2021, 8, 21, 23, 37, 21, 309, DateTimeKind.Local).AddTicks(2667) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPaid",
                table: "Purchase",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c51a325a-f0e4-48e4-8bc5-f4d8a6253b24", new DateTime(2021, 8, 21, 22, 53, 30, 637, DateTimeKind.Local).AddTicks(3811) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "725d680d-d039-4b61-a1d5-a37d7ba0e9c8", new DateTime(2021, 8, 21, 22, 53, 30, 637, DateTimeKind.Local).AddTicks(3880) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8786af9d-e69a-4a95-bed7-26b331e7fd50", new DateTime(2021, 8, 21, 22, 53, 30, 634, DateTimeKind.Local).AddTicks(4696) });
        }
    }
}
