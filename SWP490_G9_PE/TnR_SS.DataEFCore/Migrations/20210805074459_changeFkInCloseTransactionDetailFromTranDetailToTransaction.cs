using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class changeFkInCloseTransactionDetailFromTranDetailToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloseTransactionDetail_TransactionDetail",
                table: "CloseTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_CloseTransactionDetails_TransactionDetailId",
                table: "CloseTransactionDetails");

            migrationBuilder.RenameColumn(
                name: "TransactionDetailId",
                table: "CloseTransactionDetails",
                newName: "TransactionId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "CloseTransactionDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "100f65de-7926-4c8a-9154-f30ee10d4ec6", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9906) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "4ac87ecf-f202-4f2f-be3a-f699529c8df7", new DateTime(2021, 8, 5, 14, 44, 58, 666, DateTimeKind.Local).AddTicks(9954) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "da6b1313-ba57-44da-9f21-503a8a65895f", new DateTime(2021, 8, 5, 14, 44, 58, 664, DateTimeKind.Local).AddTicks(5886) });

            migrationBuilder.CreateIndex(
                name: "IX_CloseTransactionDetails_TransactionId",
                table: "CloseTransactionDetails",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CloseTransactionDetail_Transaction",
                table: "CloseTransactionDetails",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CloseTransactionDetail_Transaction",
                table: "CloseTransactionDetails");

            migrationBuilder.DropIndex(
                name: "IX_CloseTransactionDetails_TransactionId",
                table: "CloseTransactionDetails");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "CloseTransactionDetails");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "CloseTransactionDetails",
                newName: "TransactionDetailId");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "16ca4425-fb4a-4569-a0f2-cc2a942a9a49", new DateTime(2021, 8, 5, 13, 46, 5, 930, DateTimeKind.Local).AddTicks(2718) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "a5033901-e57c-4121-9755-a15815608f1b", new DateTime(2021, 8, 5, 13, 46, 5, 930, DateTimeKind.Local).AddTicks(2810) });

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "UpdatedAt" },
                values: new object[] { "850dbc9e-e2ef-4f63-a8c7-e8d377301179", new DateTime(2021, 8, 5, 13, 46, 5, 927, DateTimeKind.Local).AddTicks(3036) });

            migrationBuilder.CreateIndex(
                name: "IX_CloseTransactionDetails_TransactionDetailId",
                table: "CloseTransactionDetails",
                column: "TransactionDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CloseTransactionDetail_TransactionDetail",
                table: "CloseTransactionDetails",
                column: "TransactionDetailId",
                principalTable: "TransactionDetail",
                principalColumn: "ID");
        }
    }
}
