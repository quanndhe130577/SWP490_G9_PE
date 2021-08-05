using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addClosePurchaseDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosePurchaseDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    BasketType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasketWeight = table.Column<double>(type: "float", nullable: false),
                    FishTypeId = table.Column<int>(type: "int", nullable: false),
                    FishName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FishTypeMinWeight = table.Column<float>(type: "real", nullable: false),
                    FishTypeMaxWeight = table.Column<float>(type: "real", nullable: false),
                    FishTypePrice = table.Column<double>(type: "float", nullable: false),
                    FishTypeTransactionPrice = table.Column<double>(type: "float", nullable: false),
                    PurchaseDetailId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosePurchaseDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClosePurchaseDetail_PurchaseDetail",
                        column: x => x.PurchaseDetailId,
                        principalTable: "PurchaseDetail",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "8001de94-66d8-4e9e-b39f-579349930c4f", new DateTime(2021, 8, 4, 0, 5, 18, 143, DateTimeKind.Local).AddTicks(7889) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "75d877c8-dd09-4ce8-9e82-9f6b55a212ad", new DateTime(2021, 8, 4, 0, 5, 18, 143, DateTimeKind.Local).AddTicks(7937) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "dfec2232-5fd1-4788-8a00-375aef4827fb", new DateTime(2021, 8, 4, 0, 5, 18, 141, DateTimeKind.Local).AddTicks(9406) });

            migrationBuilder.CreateIndex(
                name: "IX_ClosePurchaseDetails_PurchaseDetailId",
                table: "ClosePurchaseDetails",
                column: "PurchaseDetailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosePurchaseDetails");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "614a184f-6ae8-488b-82bc-f45357101ff9", new DateTime(2021, 8, 2, 23, 15, 45, 381, DateTimeKind.Local).AddTicks(9810) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "9a6c19e2-f9e6-4967-aaaa-c66edb32734c", new DateTime(2021, 8, 2, 23, 15, 45, 381, DateTimeKind.Local).AddTicks(9860) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "edec0cfe-45c7-4280-b91b-9e3f08a09ac3", new DateTime(2021, 8, 2, 23, 15, 45, 358, DateTimeKind.Local).AddTicks(7330) });
        }
    }
}
