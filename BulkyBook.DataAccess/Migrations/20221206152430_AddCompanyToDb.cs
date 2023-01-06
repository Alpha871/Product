using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkyBook.DataAccess.Migrations
{
    public partial class AddCompanyToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreeAddress",
                table: "Companies",
                newName: "StreetAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Companies",
                newName: "StreeAddress");
        }
    }
}
