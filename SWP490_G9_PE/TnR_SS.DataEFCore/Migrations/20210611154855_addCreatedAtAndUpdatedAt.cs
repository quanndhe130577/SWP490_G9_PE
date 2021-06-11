using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addCreatedAtAndUpdatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "UserInfor",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "TienGioiThieu",
                table: "Purchase",
                newName: "Commission");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Purchase",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserInfor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RoleUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "RoleUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PurchaseDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PurchaseDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Createdat",
                table: "Purchase",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PondOwner",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PondOwner",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b326031b-0f34-40b6-83d9-cce6f1671ffa");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "af226bc0-4337-4775-a5ec-398c48ab8f7c");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8d77bf9f-487e-4bb6-9aaa-2ed947d8c92b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserInfor");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RoleUser");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "Createdat",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PondOwner");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PondOwner");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserInfor",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Purchase",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "Commission",
                table: "Purchase",
                newName: "TienGioiThieu");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d53a6346-d771-403e-8af1-b355aa87d518");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "813afca5-a812-472e-91c7-1cfdf1a6b6dc");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "91245791-e09e-46cb-b985-5a898e305984");
        }
    }
}
