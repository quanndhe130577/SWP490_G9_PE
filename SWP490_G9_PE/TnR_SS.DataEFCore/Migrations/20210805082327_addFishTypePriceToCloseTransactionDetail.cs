using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addFishTypePriceToCloseTransactionDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FishTypePrice",
                table: "CloseTransactionDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2cdaa15d-21c4-49fe-b62a-4b77490d38bb", new DateTime(2021, 8, 5, 15, 23, 27, 191, DateTimeKind.Local).AddTicks(4872) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8592b189-f595-4845-a072-617d481b0c46", new DateTime(2021, 8, 5, 15, 23, 27, 191, DateTimeKind.Local).AddTicks(4911) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c5df0b7b-a988-4a0c-8a76-7f5e6b535e3e", new DateTime(2021, 8, 5, 15, 23, 27, 189, DateTimeKind.Local).AddTicks(3947) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FishTypePrice",
                table: "CloseTransactionDetails");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "100f65de-7926-4c8a-9154-f30ee10d4ec6", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9906) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4ac87ecf-f202-4f2f-be3a-f699529c8df7", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9954) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "da6b1313-ba57-44da-9f21-503a8a65895f", new DateTime(2021, 8, 5, 14, 44, 58, 664, DateTimeKind.Local).AddTicks(5886) });
        }
    }
}
