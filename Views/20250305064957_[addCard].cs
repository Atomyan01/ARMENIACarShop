using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMENIACarShop.Migrations
{
    /// <inheritdoc />
    public partial class addCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_Buyer_BuyerModelId",
                table: "OrderModel");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_PaymentModel_PaymentId",
                table: "OrderModel");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderModel_SellerModel_SellerModelId",
                table: "OrderModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderModel",
                table: "OrderModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderModel_BuyerModelId",
                table: "OrderModel");

            migrationBuilder.DropColumn(
                name: "BuyerModelId",
                table: "OrderModel");

            migrationBuilder.RenameTable(
                name: "OrderModel",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderModel_SellerModelId",
                table: "Orders",
                newName: "IX_Orders_SellerModelId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderModel_PaymentId",
                table: "Orders",
                newName: "IX_Orders_PaymentId");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "CarModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_OrderModelId",
                table: "CarModel",
                column: "OrderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModel_Orders_OrderModelId",
                table: "CarModel",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Buyer_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentModel_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "PaymentModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SellerModel_SellerModelId",
                table: "Orders",
                column: "SellerModelId",
                principalTable: "SellerModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarModel_Orders_OrderModelId",
                table: "CarModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buyer_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentModel_PaymentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SellerModel_SellerModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CarModel_OrderModelId",
                table: "CarModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "CarModel");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "OrderModel");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_SellerModelId",
                table: "OrderModel",
                newName: "IX_OrderModel_SellerModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentId",
                table: "OrderModel",
                newName: "IX_OrderModel_PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "BuyerModelId",
                table: "OrderModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderModel",
                table: "OrderModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderModel_BuyerModelId",
                table: "OrderModel",
                column: "BuyerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_Buyer_BuyerModelId",
                table: "OrderModel",
                column: "BuyerModelId",
                principalTable: "Buyer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_PaymentModel_PaymentId",
                table: "OrderModel",
                column: "PaymentId",
                principalTable: "PaymentModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderModel_SellerModel_SellerModelId",
                table: "OrderModel",
                column: "SellerModelId",
                principalTable: "SellerModel",
                principalColumn: "Id");
        }
    }
}
