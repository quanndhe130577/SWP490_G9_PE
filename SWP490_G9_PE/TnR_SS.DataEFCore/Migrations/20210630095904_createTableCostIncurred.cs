using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class createTableCostIncurred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalWeight",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "LK_PurchaseDeatil_Drum");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "BuyPrice",
                table: "PurchaseDetail",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Employee",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "CostIncurred",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostIncurred", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CostIncurred_UserInfor",
                        column: x => x.UserId,
                        principalTable: "UserInfor",
                        principalColumn: "ID");
                });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "dc3a595e-efd6-47bd-b810-37cb9308da3c");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a4e03a39-f266-4bfb-b0a5-2559b509b6fd");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "857a84f2-13b2-40c4-b6d6-42cb24bd9b04");

            migrationBuilder.CreateIndex(
                name: "IX_CostIncurred_UserId",
                table: "CostIncurred",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropTable(
                name: "CostIncurred");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "PurchaseDetail",
                newName: "BuyPrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Employee",
                newName: "FirstName");

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

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "LK_PurchaseDeatil_Drum",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ecc2d8e9-e21d-4b6d-bdec-d271db99dff1");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "46290eeb-c3f8-4efd-874b-ec751b8070a7");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5b615327-0e95-43cb-bc66-79f012ae1de0");

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
