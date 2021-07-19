using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class changeWeightRecorderIdInBuyerToSellerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeightRecorderId",
                table: "Buyer",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Buyer_WeightRecorderId",
                table: "Buyer",
                newName: "IX_Buyer_SellerId");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "27d28b87-0166-4c05-97f1-d3a3556a3a54", new DateTime(2021, 7, 19, 11, 31, 14, 372, DateTimeKind.Local).AddTicks(3409) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2dc61e86-d2c2-4296-a29e-188eeb1f3043", new DateTime(2021, 7, 19, 11, 31, 14, 372, DateTimeKind.Local).AddTicks(3518) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9456ffb8-f306-490e-ba70-8c0203c52f0a", new DateTime(2021, 7, 19, 11, 31, 14, 370, DateTimeKind.Local).AddTicks(7777) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Buyer",
                newName: "WeightRecorderId");

            migrationBuilder.RenameIndex(
                name: "IX_Buyer_SellerId",
                table: "Buyer",
                newName: "IX_Buyer_WeightRecorderId");

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
    }
}
