using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addWeightRecorderIdAndFkToBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Buyer]", true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Employee",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "WeightRecorderId",
                table: "Buyer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1cafc30a-e989-4274-9a70-3aef541e2005");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ce5befb4-7a3a-419b-ad23-5f76c697de46");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "2fee0fe0-ce50-4e4a-b4a2-293362d2b9b1");

            migrationBuilder.CreateIndex(
                name: "IX_Buyer_WeightRecorderId",
                table: "Buyer",
                column: "WeightRecorderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyer_UserInfor",
                table: "Buyer",
                column: "WeightRecorderId",
                principalTable: "UserInfor",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyer_UserInfor",
                table: "Buyer");

            migrationBuilder.DropIndex(
                name: "IX_Buyer_WeightRecorderId",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "WeightRecorderId",
                table: "Buyer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Employee",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e7d2116c-d47f-45f1-bd6a-54583e531121");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2f542a44-3694-4e2e-afe2-66e23c05c23c");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "04ea901e-396c-43ca-9ca9-5675c2272c2a");
        }
    }
}
