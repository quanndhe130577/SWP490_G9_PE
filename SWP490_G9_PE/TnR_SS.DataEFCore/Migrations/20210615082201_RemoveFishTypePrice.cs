using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class RemoveFishTypePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishTypePrice",
                table: "PurchaseDetail");

            migrationBuilder.DropTable(
                name: "FishTypePrice");

            migrationBuilder.RenameColumn(
                name: "FishTypePriceID",
                table: "PurchaseDetail",
                newName: "FishTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_FishTypePriceID",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_FishTypeID");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "FishType",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "FishType",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8fa78a6e-db42-468a-8173-8b952d46656b");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0d372e2b-20f1-4421-b6a4-9c0bba0b73fe");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3558fd9e-e470-411d-9cca-aebb88596492");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail",
                column: "FishTypeID",
                principalTable: "FishType",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "FishType");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "FishType");

            migrationBuilder.RenameColumn(
                name: "FishTypeID",
                table: "PurchaseDetail",
                newName: "FishTypePriceID");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_FishTypeID",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_FishTypePriceID");

            migrationBuilder.CreateTable(
                name: "FishTypePrice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    FishTypeID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishTypePrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FishTypePrice_FishType",
                        column: x => x.FishTypeID,
                        principalTable: "FishType",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7a6d1943-292c-47dc-810d-452a54a47941");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1d8e56fc-02ea-4254-9ee1-d36f151a8876");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d3ed689a-1a22-46b0-860a-dc1600a615a1");

            migrationBuilder.CreateIndex(
                name: "IX_FishTypePrice_FishTypeID",
                table: "FishTypePrice",
                column: "FishTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishTypePrice",
                table: "PurchaseDetail",
                column: "FishTypePriceID",
                principalTable: "FishTypePrice",
                principalColumn: "ID");
        }
    }
}
