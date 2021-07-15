using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class RemovePondOwnerIdInFishTypeAndChangeEndDateToNullableInHistorySalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishType_PondOwner",
                table: "FishType");

            migrationBuilder.DropIndex(
                name: "IX_FishType_PondOwnerID",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "PondOwnerID",
                table: "FishType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "HistorySalaryEmp",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HistorySalaryEmp",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));


            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "d8531425-55e8-4720-9bfc-ca8bf452f986", new DateTime(2021, 7, 14, 22, 33, 6, 654, DateTimeKind.Local).AddTicks(7500) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "967b3ece-1e70-4036-9831-a5010910cd42", new DateTime(2021, 7, 14, 22, 33, 6, 654, DateTimeKind.Local).AddTicks(7546) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "e5659458-419b-4a74-8bfc-bfcb911dbd0b", new DateTime(2021, 7, 14, 22, 33, 6, 653, DateTimeKind.Local).AddTicks(451) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HistorySalaryEmp");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HistorySalaryEmp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "HistorySalaryEmp",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PondOwnerID",
                table: "FishType",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "6d6dc15f-bdc6-47ea-b36d-3ea6bd252597", new DateTime(2021, 7, 14, 0, 26, 15, 281, DateTimeKind.Local).AddTicks(6041) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c7de3235-8353-4303-8d0c-c0fa760991d6", new DateTime(2021, 7, 14, 0, 26, 15, 281, DateTimeKind.Local).AddTicks(6074) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "22c19dbf-2dd9-4ef3-a001-753338fa6243", new DateTime(2021, 7, 14, 0, 26, 15, 280, DateTimeKind.Local).AddTicks(5113) });
          
        }
    }
}
