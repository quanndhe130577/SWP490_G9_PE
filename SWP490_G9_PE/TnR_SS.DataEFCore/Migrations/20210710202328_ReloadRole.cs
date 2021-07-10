using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class ReloadRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DisplayName", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { "028af069-e57d-4e54-b113-c012526c0ea3", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "ADMIN", new DateTime(2021, 7, 11, 3, 23, 28, 22, DateTimeKind.Local).AddTicks(5786) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DisplayName", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { "3f213f71-9eb1-40fb-9cb2-d8a056a363eb", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thương lái", "Trader", "TRADER", new DateTime(2021, 7, 11, 3, 23, 28, 22, DateTimeKind.Local).AddTicks(5825) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DisplayName", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { "640c72b0-4e7a-42c8-b257-c95cb24ebd28", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chủ bến", "WeightRecorder", "WEIGHTRECORDER", new DateTime(2021, 7, 11, 3, 23, 28, 21, DateTimeKind.Local).AddTicks(2751) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DisplayName", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { "178e7222-2af1-46b0-bf54-57ae3887f082", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "Admin", "ADMIN", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "DisplayName", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[] { "2dfd4946-ce93-47f1-8ec8-1f1d80cdfb86", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thương lái", "Trader", "TRADER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "NormalizedName", "UpdatedAt" },
                values: new object[] { "d5491626-0928-437b-914a-f883974c5e4e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "WEIGHT RECORDER", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
