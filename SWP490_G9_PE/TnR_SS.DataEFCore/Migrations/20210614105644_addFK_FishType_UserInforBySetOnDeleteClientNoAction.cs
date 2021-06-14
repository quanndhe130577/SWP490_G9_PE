using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class addFK_FishType_UserInforBySetOnDeleteClientNoAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Basket",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_TongKetMua",
                table: "PurchaseDetail");

            migrationBuilder.RenameColumn(
                name: "TongKetMuaId",
                table: "PurchaseDetail",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_TongKetMuaId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_PurchaseId");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "132668e1-70d2-424c-a955-06bbde91a34b");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c9ff4d9b-853e-4e76-b6f1-4d24aec9b124");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "53776147-563e-4bad-baf0-9c6121e5c703");

            migrationBuilder.CreateIndex(
                name: "IX_FishType_TraderID",
                table: "FishType",
                column: "TraderID");

            migrationBuilder.AddForeignKey(
                name: "FK_FishType_UserInfor",
                table: "FishType",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Basket",
                table: "PurchaseDetail",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail",
                column: "FishTypeID",
                principalTable: "FishType",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Purchase",
                table: "PurchaseDetail",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishType_UserInfor",
                table: "FishType");

            migrationBuilder.DropForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Basket",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Purchase",
                table: "PurchaseDetail");

            migrationBuilder.DropIndex(
                name: "IX_FishType_TraderID",
                table: "FishType");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "PurchaseDetail",
                newName: "TongKetMuaId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_PurchaseId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_TongKetMuaId");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2bc3bf1a-ca0d-4d82-a382-824720816f94");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "59de8a88-cc47-4b5b-b7fb-643d6cb644ac");

            migrationBuilder.UpdateData(
                table: "RoleUser",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6ec51015-d001-4331-908d-dcae2ee39c8e");

            migrationBuilder.AddForeignKey(
                name: "FK_PondOwner_UserInfor",
                table: "PondOwner",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_PondOwner",
                table: "Purchase",
                column: "PondOwnerID",
                principalTable: "PondOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_UserInfor",
                table: "Purchase",
                column: "TraderID",
                principalTable: "UserInfor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Basket",
                table: "PurchaseDetail",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_FishType",
                table: "PurchaseDetail",
                column: "FishTypeID",
                principalTable: "FishType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_TongKetMua",
                table: "PurchaseDetail",
                column: "TongKetMuaId",
                principalTable: "Purchase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
