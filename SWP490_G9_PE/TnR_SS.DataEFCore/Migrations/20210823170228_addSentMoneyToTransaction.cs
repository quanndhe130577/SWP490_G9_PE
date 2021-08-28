using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addSentMoneyToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SentMoney",
                table: "Transaction",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "68ca7f2f-c17e-4d46-8af9-4d83a2c09bc2", new DateTime(2021, 8, 24, 0, 2, 27, 671, DateTimeKind.Local).AddTicks(2445) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "1cdaf0b7-8e2b-4b31-b31c-25a6ada24996", new DateTime(2021, 8, 24, 0, 2, 27, 671, DateTimeKind.Local).AddTicks(2506) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8954eb07-0a82-4845-a057-ea6aa98f356d", new DateTime(2021, 8, 24, 0, 2, 27, 668, DateTimeKind.Local).AddTicks(9974) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentMoney",
                table: "Transaction");

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
    }
}
