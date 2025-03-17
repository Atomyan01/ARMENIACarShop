using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMENIACarShop.Migrations
{
    /// <inheritdoc />
    public partial class addOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buyer_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BuyerModelId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalCars",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerModelId",
                table: "Orders",
                column: "BuyerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Buyer_BuyerModelId",
                table: "Orders",
                column: "BuyerModelId",
                principalTable: "Buyer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Buyer_BuyerModelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerModelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalCars",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Buyer_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Buyer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
