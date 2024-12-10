using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSLoggerAPI.Migrations
{
    public partial class init_loggerdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLogger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoggingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggingProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoggingClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoggingMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLogger", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLogger");
        }
    }
}
