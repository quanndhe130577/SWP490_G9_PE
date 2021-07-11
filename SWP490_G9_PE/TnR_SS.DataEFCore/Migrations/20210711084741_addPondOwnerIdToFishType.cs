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
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "937c40c0-34cb-4717-9067-b24965827c58", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6347) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "35921199-c862-4b85-aae8-9a47f3afbd9e", new DateTime(2021, 7, 11, 15, 47, 41, 221, DateTimeKind.Local).AddTicks(6383) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "570e1e8c-c874-42fc-b1ec-5c2068b65711", new DateTime(2021, 7, 11, 15, 47, 41, 219, DateTimeKind.Local).AddTicks(6929) });

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
