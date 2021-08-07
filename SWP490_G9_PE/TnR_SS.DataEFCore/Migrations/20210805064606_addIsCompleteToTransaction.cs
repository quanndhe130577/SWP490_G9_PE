using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addIsCompleteToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "isCompleted",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "16ca4425-fb4a-4569-a0f2-cc2a942a9a49", new DateTime(2021, 8, 5, 13, 46, 5, 930, DateTimeKind.Local).AddTicks(2718) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a5033901-e57c-4121-9755-a15815608f1b", new DateTime(2021, 8, 5, 13, 46, 5, 930, DateTimeKind.Local).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "850dbc9e-e2ef-4f63-a8c7-e8d377301179", new DateTime(2021, 8, 5, 13, 46, 5, 927, DateTimeKind.Local).AddTicks(3036) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "Transaction");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "bf9edf7d-17f0-4e9e-a816-2a0b4c48d335", new DateTime(2021, 8, 5, 13, 35, 3, 30, DateTimeKind.Local).AddTicks(1446) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e1148d50-c1b1-4b4e-833b-1c748cf24bf6", new DateTime(2021, 8, 5, 13, 35, 3, 30, DateTimeKind.Local).AddTicks(1629) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "14bd2fc3-9d88-4d38-81ea-eedf5efb366e", new DateTime(2021, 8, 5, 13, 35, 3, 27, DateTimeKind.Local).AddTicks(8452) });
        }
    }
}
