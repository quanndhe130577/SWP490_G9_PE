using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addSentMoneyToPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PondBackMoney",
                table: "Purchase",
                newName: "SentMoney");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SentMoney",
                table: "Purchase",
                newName: "PondBackMoney");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4cee5633-c0b3-43a2-8377-940ceee76ba2", new DateTime(2021, 8, 20, 10, 20, 35, 20, DateTimeKind.Local).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "74813782-7d03-4fc7-a926-78e5734d0ee7", new DateTime(2021, 8, 20, 10, 20, 35, 20, DateTimeKind.Local).AddTicks(4705) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "312426fd-d101-4628-a626-ec175c6cfb6d", new DateTime(2021, 8, 20, 10, 20, 35, 18, DateTimeKind.Local).AddTicks(376) });
        }
    }
}
