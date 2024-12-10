using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSProductAPI.Migrations
{
    public partial class product_rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEffectiveRateDetail");

            migrationBuilder.DropTable(
                name: "ProductEffectiveRate");

            migrationBuilder.CreateTable(
                name: "ProductRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRate_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRate_ProductId",
                table: "ProductRate",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRate");

            migrationBuilder.CreateTable(
                name: "ProductEffectiveRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEffectiveRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductEffectiveRateDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductEffectiveRateId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EffectiveRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEffectiveRateDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEffectiveRateDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductEffectiveRateDetail_ProductEffectiveRate_ProductEffectiveRateId",
                        column: x => x.ProductEffectiveRateId,
                        principalTable: "ProductEffectiveRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEffectiveRateDetail_ProductEffectiveRateId",
                table: "ProductEffectiveRateDetail",
                column: "ProductEffectiveRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEffectiveRateDetail_ProductId",
                table: "ProductEffectiveRateDetail",
                column: "ProductId");
        }
    }
}
