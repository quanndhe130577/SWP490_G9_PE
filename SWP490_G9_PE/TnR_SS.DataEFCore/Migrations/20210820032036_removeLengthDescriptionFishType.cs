using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class removeLengthDescriptionFishType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FishType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FishType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e6933cab-3ca0-4edd-a9f9-49e1ecc20608", new DateTime(2021, 8, 16, 21, 26, 8, 943, DateTimeKind.Local).AddTicks(2500) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4c163e4d-80fc-4fa1-932b-fc11667a3774", new DateTime(2021, 8, 16, 21, 26, 8, 943, DateTimeKind.Local).AddTicks(2560) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "3a650bc2-2e65-464a-82f0-6de9f69afe2c", new DateTime(2021, 8, 16, 21, 26, 8, 929, DateTimeKind.Local).AddTicks(6210) });
        }
    }
}
