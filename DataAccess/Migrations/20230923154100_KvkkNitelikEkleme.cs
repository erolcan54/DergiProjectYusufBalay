using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class KvkkNitelikEkleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "KVKK",
                table: "OzelDersVeliBasvuru",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KVKK",
                table: "Okul",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KVKK",
                table: "isBasvuru",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KVKK",
                table: "BurslulukSinavBasvuru",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KVKK",
                table: "OzelDersVeliBasvuru");

            migrationBuilder.DropColumn(
                name: "KVKK",
                table: "Okul");

            migrationBuilder.DropColumn(
                name: "KVKK",
                table: "isBasvuru");

            migrationBuilder.DropColumn(
                name: "KVKK",
                table: "BurslulukSinavBasvuru");
        }
    }
}
