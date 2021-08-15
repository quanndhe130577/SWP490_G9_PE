using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class changeSomeThingToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "PurchaseDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PondOwnerID",
                table: "Purchase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "801c3b00-8dc2-4324-b38a-33a5ed429011", new DateTime(2021, 8, 15, 23, 48, 53, 984, DateTimeKind.Local).AddTicks(1537) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "b833ba9a-dca5-478d-8c30-cbbfa8754a10", new DateTime(2021, 8, 15, 23, 48, 53, 984, DateTimeKind.Local).AddTicks(1602) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "894395e2-0233-4558-9ce5-f3ee6a3ed1c0", new DateTime(2021, 8, 15, 23, 48, 53, 981, DateTimeKind.Local).AddTicks(9235) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BasketId",
                table: "PurchaseDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PondOwnerID",
                table: "Purchase",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9ae2345c-ce21-430e-a9e3-d521782de89e", new DateTime(2021, 8, 15, 20, 43, 16, 898, DateTimeKind.Local).AddTicks(150) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4679db98-1363-4ace-b31f-b746b8755b0f", new DateTime(2021, 8, 15, 20, 43, 16, 898, DateTimeKind.Local).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2712ba23-49c8-40f9-9852-3973c304352d", new DateTime(2021, 8, 15, 20, 43, 16, 895, DateTimeKind.Local).AddTicks(9861) });
        }
    }
}
