using Microsoft.EntityFrameworkCore.Migrations;

namespace TnR_SS.DataEFCore.Migrations
{
    public partial class renameRoToBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBuy_Ro",
                table: "TransactionBuy");

            migrationBuilder.DropTable(
                name: "Ro");

            migrationBuilder.RenameColumn(
                name: "RoId",
                table: "TransactionBuy",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionBuy_RoId",
                table: "TransactionBuy",
                newName: "IX_TransactionBuy_BasketId");

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nchar", fixedLength: true, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "22cd6048-e999-4a19-9276-f03ef37afc2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "caab21d4-f700-43df-a709-1f3deafc62fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "86d82f5e-2f80-4fcb-9b03-30c2807eb65d");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBuy_Basket",
                table: "TransactionBuy",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionBuy_Basket",
                table: "TransactionBuy");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "TransactionBuy",
                newName: "RoId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionBuy_BasketId",
                table: "TransactionBuy",
                newName: "IX_TransactionBuy_RoId");

            migrationBuilder.CreateTable(
                name: "Ro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nchar", fixedLength: true, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ro", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5e6218e6-64b4-4610-b4ae-b5ccaff785b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "867fe0f6-dfe5-44aa-8e21-f6bb676dc8a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "f5b4bd94-d7a3-4728-bc3a-3f7d2bee8413");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionBuy_Ro",
                table: "TransactionBuy",
                column: "RoId",
                principalTable: "Ro",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
