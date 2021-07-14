using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addTraderOfWeightRecorderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TraderOfWeightRecorder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraderId = table.Column<int>(type: "int", nullable: false),
                    WeightRecorderId = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraderOfWeightRecorder", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TraderOfWeightRecorder_Trader",
                        column: x => x.TraderId,
                        principalTable: "UserInfor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TraderOfWeightRecorder_WeightRecorder",
                        column: x => x.WeightRecorderId,
                        principalTable: "UserInfor",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "be1bc5d6-d09e-4aba-9d3d-102458925def", new DateTime(2021, 7, 14, 1, 39, 13, 839, DateTimeKind.Local).AddTicks(7324) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "ba0c477d-3750-465f-929a-c6e2cfd7be21", new DateTime(2021, 7, 14, 1, 39, 13, 839, DateTimeKind.Local).AddTicks(7420) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "67992609-8f80-443c-b29f-bc4c94c39024", new DateTime(2021, 7, 14, 1, 39, 13, 837, DateTimeKind.Local).AddTicks(7346) });

            migrationBuilder.CreateIndex(
                name: "IX_TraderOfWeightRecorder_TraderId",
                table: "TraderOfWeightRecorder",
                column: "TraderId");

            migrationBuilder.CreateIndex(
                name: "IX_TraderOfWeightRecorder_WeightRecorderId",
                table: "TraderOfWeightRecorder",
                column: "WeightRecorderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TraderOfWeightRecorder");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "763710bd-0140-474c-aaaf-ef4db5382bca", new DateTime(2021, 7, 14, 0, 41, 3, 221, DateTimeKind.Local).AddTicks(9929) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c1d5d471-a5f4-4f84-b698-5834b933d7d0", new DateTime(2021, 7, 14, 0, 41, 3, 221, DateTimeKind.Local).AddTicks(9987) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "c76e2d7b-ef99-4706-8669-14fa8e0274fd", new DateTime(2021, 7, 14, 0, 41, 3, 220, DateTimeKind.Local).AddTicks(1125) });
        }
    }
}
