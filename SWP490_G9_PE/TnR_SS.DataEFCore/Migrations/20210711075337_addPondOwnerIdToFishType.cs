using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addPondOwnerIdToFishType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PondOwnerID",
                table: "FishType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "2420c0da-562e-47c2-b571-5054c799e59a", new DateTime(2021, 7, 11, 14, 53, 36, 936, DateTimeKind.Local).AddTicks(4048) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "53a06dfa-9e6e-42dd-a09b-18a8aac8b853", new DateTime(2021, 7, 11, 14, 53, 36, 936, DateTimeKind.Local).AddTicks(4162) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4cc06146-1da5-4621-911a-9590d7177c44", new DateTime(2021, 7, 11, 14, 53, 36, 934, DateTimeKind.Local).AddTicks(2585) });

            migrationBuilder.CreateIndex(
                name: "IX_FishType_PondOwnerID",
                table: "FishType",
                column: "PondOwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_FishType_PondOwner",
                table: "FishType",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "028af069-e57d-4e54-b113-c012526c0ea3", new DateTime(2021, 7, 11, 3, 23, 28, 22, DateTimeKind.Local).AddTicks(5786) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "3f213f71-9eb1-40fb-9cb2-d8a056a363eb", new DateTime(2021, 7, 11, 3, 23, 28, 22, DateTimeKind.Local).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "640c72b0-4e7a-42c8-b257-c95cb24ebd28", new DateTime(2021, 7, 11, 3, 23, 28, 21, DateTimeKind.Local).AddTicks(2751) });
        }
    }
}
