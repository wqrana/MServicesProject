using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSAuthenticationAPI.Migrations
{
    public partial class first_name_corr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FristName",
                table: "ApplicationUser",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ApplicationUser",
                newName: "FristName");
        }
    }
}
