using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARMENIACarShop.Migrations
{
    /// <inheritdoc />
    public partial class meqena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvarageScore",
                table: "PersonModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    buyerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewModel_PersonModel_SellerId",
                        column: x => x.SellerId,
                        principalTable: "PersonModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewModel_PersonModel_buyerId",
                        column: x => x.buyerId,
                        principalTable: "PersonModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModel_buyerId",
                table: "ReviewModel",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModel_SellerId",
                table: "ReviewModel",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewModel");

            migrationBuilder.AlterColumn<double>(
                name: "AvarageScore",
                table: "PersonModel",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
