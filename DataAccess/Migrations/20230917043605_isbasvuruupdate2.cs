using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class isbasvuruupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ilceId",
                table: "isBasvuru");

            migrationBuilder.AddColumn<string>(
                name: "Hakkinda",
                table: "isBasvuru",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hakkinda",
                table: "isBasvuru");

            migrationBuilder.AddColumn<int>(
                name: "ilceId",
                table: "isBasvuru",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
