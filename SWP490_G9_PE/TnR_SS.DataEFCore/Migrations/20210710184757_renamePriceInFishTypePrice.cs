using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class renamePriceInFishTypePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellPrice",
                table: "FishTypePrice",
                newName: "TransactionPrice");

            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                table: "FishTypePrice",
                newName: "PurchasePrice");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cad3ca25-0c53-4ffb-b503-dd84c9780d87");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "21e2405e-de08-43ac-81e3-110cf03eef27");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c574ffc7-12e6-4d9f-8eec-1218084caa4b");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionPrice",
                table: "FishTypePrice",
                newName: "SellPrice");

            migrationBuilder.RenameColumn(
                name: "PurchasePrice",
                table: "FishTypePrice",
                newName: "BuyPrice");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a8c07617-f9f9-4441-8910-8607f1043667");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1680d301-4b7e-40a7-8512-a09234b822a3");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3e5c1924-18f7-4ffe-acb7-fbb7f2d7da7e");
        }
    }
}
