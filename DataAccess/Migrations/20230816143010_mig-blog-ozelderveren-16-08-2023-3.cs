using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class migblogozelderveren160820233 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resim = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Konu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hit = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "OzelDersOgretmen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resim = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BransId = table.Column<int>(type: "int", nullable: false),
                    ilId = table.Column<int>(type: "int", nullable: false),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    Yuzyuze = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OzelDersOgretmen", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");


            migrationBuilder.DropTable(
                name: "OzelDersOgretmen");
        }
    }
}
