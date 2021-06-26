using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class remove2TotalInPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner_PondOwnerID",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Purchase");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1f8134d1-1db9-41e2-a361-148aa0cd6d86");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3c317a17-cdf3-4a50-a887-54c26eeef3ad");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9b5f6d35-4a86-4b50-a5f3-24b1df58f36f");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase");

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "Purchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalWeight",
                table: "Purchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "336596af-a610-4513-8dab-01f4cb34f0cb");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9d098fba-1cb4-48f5-ae85-4067cb936be1");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ef7f5606-629a-40b1-bd15-f235221946fe");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner_PondOwnerID",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
