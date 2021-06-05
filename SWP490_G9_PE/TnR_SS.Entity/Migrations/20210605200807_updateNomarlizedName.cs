using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.Entity.Migrations
{
    public partial class updateNomarlizedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b036c01b-5972-4166-9449-6d39be3f1072", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "f498a0fd-3c57-4675-8ffe-38fcd724faa3", "Trader" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "1a92e729-a8be-405e-8c52-0cfb25b255aa", "Weight Recorder" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "d43d871f-b57a-484a-96ad-fe15cf8f982d", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "18eafe2a-935d-43f2-9541-3c7372ce2313", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "b17c8c65-5b4b-41e0-b646-997fdc70ebf0", null });
        }
    }
}
