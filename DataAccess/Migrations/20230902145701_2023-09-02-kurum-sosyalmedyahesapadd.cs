using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class _20230902kurumsosyalmedyahesapadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Okul",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Okul",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Okul",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Whatsapp",
                table: "Okul",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Okul");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Okul");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Okul");

            migrationBuilder.DropColumn(
                name: "Whatsapp",
                table: "Okul");
        }
    }
}
