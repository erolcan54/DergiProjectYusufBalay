using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class cvPdfchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdSoyad",
                table: "isBasvuru");

            migrationBuilder.DropColumn(
                name: "BransId",
                table: "isBasvuru");

            migrationBuilder.DropColumn(
                name: "ilId",
                table: "isBasvuru");

            migrationBuilder.RenameColumn(
                name: "Resim",
                table: "isBasvuru",
                newName: "CvPDF");

            migrationBuilder.RenameColumn(
                name: "Hakkinda",
                table: "isBasvuru",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "isBasvuru",
                newName: "Ad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "isBasvuru",
                newName: "Hakkinda");

            migrationBuilder.RenameColumn(
                name: "CvPDF",
                table: "isBasvuru",
                newName: "Resim");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "isBasvuru",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "AdSoyad",
                table: "isBasvuru",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BransId",
                table: "isBasvuru",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ilId",
                table: "isBasvuru",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
