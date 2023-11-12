using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class SliderPopuModalKurumsalEntityAdd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "Kurumsal",
                schema:"dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurumsal", x => x.Id);
                });



            migrationBuilder.CreateTable(
                name: "PopupModal",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resim = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SonTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopupModal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resim = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basari");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Brans");

            migrationBuilder.DropTable(
                name: "BurslulukSinav");

            migrationBuilder.DropTable(
                name: "BurslulukSinavBasvuru");

            migrationBuilder.DropTable(
                name: "DisGorsel");

            migrationBuilder.DropTable(
                name: "EgitimModeli");

            migrationBuilder.DropTable(
                name: "EgitimModeliResim");

            migrationBuilder.DropTable(
                name: "EgitimTur");

            migrationBuilder.DropTable(
                name: "Etkinlik");

            migrationBuilder.DropTable(
                name: "EtkinlikResim");

            migrationBuilder.DropTable(
                name: "IcGorsel");

            migrationBuilder.DropTable(
                name: "il");

            migrationBuilder.DropTable(
                name: "ilce");

            migrationBuilder.DropTable(
                name: "indirim");

            migrationBuilder.DropTable(
                name: "isBasvuru");

            migrationBuilder.DropTable(
                name: "Katalog");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Kulup");

            migrationBuilder.DropTable(
                name: "KurumBeniAra");

            migrationBuilder.DropTable(
                name: "Kurumsal");

            migrationBuilder.DropTable(
                name: "KurumYorum");

            migrationBuilder.DropTable(
                name: "KurumYorumBegeni");

            migrationBuilder.DropTable(
                name: "Ogretmen");

            migrationBuilder.DropTable(
                name: "Okul");

            migrationBuilder.DropTable(
                name: "OkulTur");

            migrationBuilder.DropTable(
                name: "OzelDersOgretmen");

            migrationBuilder.DropTable(
                name: "OzelDersVeliBasvuru");

            migrationBuilder.DropTable(
                name: "OzelOgretmenYorum");

            migrationBuilder.DropTable(
                name: "OzelOgretmenYorumBegeni");

            migrationBuilder.DropTable(
                name: "PopupModal");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropTable(
                name: "Yonetici");
        }
    }
}
