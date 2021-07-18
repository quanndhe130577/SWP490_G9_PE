using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class editNullableForWcIdInTransactionAndBuyerInTransactionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "TransactionDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WeightRecorderId",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f170240d-418b-496c-af6b-8538e2363766", new DateTime(2021, 7, 18, 1, 27, 40, 920, DateTimeKind.Local).AddTicks(6547) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "308d3956-05f6-4b32-a6b7-4c3e52fb5efd", new DateTime(2021, 7, 18, 1, 27, 40, 920, DateTimeKind.Local).AddTicks(6599) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "fa9bcbf6-8e6f-494f-a719-8ad21950b69c", new DateTime(2021, 7, 18, 1, 27, 40, 919, DateTimeKind.Local).AddTicks(243) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BuyerId",
                table: "TransactionDetail",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WeightRecorderId",
                table: "Transaction",
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
                values: new object[] { "2c21961b-264a-460a-ae5d-19fc52618850", new DateTime(2021, 7, 16, 20, 39, 3, 64, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "f627206a-e86b-4482-bfaa-433b51f4394d", new DateTime(2021, 7, 16, 20, 39, 3, 64, DateTimeKind.Local).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "0091eac6-3f46-4d42-a352-d83e80112505", new DateTime(2021, 7, 16, 20, 39, 3, 50, DateTimeKind.Local).AddTicks(5230) });
        }
    }
}
