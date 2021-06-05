using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class addDisplayRoleName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "NormalizedName" },
                values: new object[] { "d43d871f-b57a-484a-96ad-fe15cf8f982d", "Admin", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "NormalizedName" },
                values: new object[] { "18eafe2a-935d-43f2-9541-3c7372ce2313", "Thương lái", "Trader" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "DisplayName", "NormalizedName" },
                values: new object[] { "b17c8c65-5b4b-41e0-b646-997fdc70ebf0", "Chủ bến", "Weight Recorder" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "2bf2693d-64f7-4015-945b-11b1dcca3024", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "811ed2cd-f71e-4366-b558-b392ddb805ae", "Thương lái" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "091fbf22-b231-4a76-8c10-576f537e809e", "Chủ bến" });
        }
    }
}
