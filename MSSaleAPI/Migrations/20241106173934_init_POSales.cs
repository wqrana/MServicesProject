using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSSaleAPI.Migrations
{
    public partial class init_POSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "POSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POSalesDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POSalesId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SalesPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalesDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSalesDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSalesDetail_POSales_POSalesId",
                        column: x => x.POSalesId,
                        principalTable: "POSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_POSalesDetail_POSalesId",
                table: "POSalesDetail",
                column: "POSalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSalesDetail");

            migrationBuilder.DropTable(
                name: "POSales");
        }
    }
}
