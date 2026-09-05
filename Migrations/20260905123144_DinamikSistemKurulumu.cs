using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkademikWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class DinamikSistemKurulumu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Akademisyenler",
                columns: table => new
                {
                    AkademisyenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdSoyad = table.Column<string>(type: "TEXT", nullable: false),
                    Unvan = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Akademisyenler", x => x.AkademisyenId);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    OgrenciNo = table.Column<string>(type: "TEXT", nullable: false),
                    Isim = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.OgrenciNo);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    Kod = table.Column<string>(type: "TEXT", nullable: false),
                    Isim = table.Column<string>(type: "TEXT", nullable: false),
                    AkademisyenId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParametrelerJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.Kod);
                    table.ForeignKey(
                        name: "FK_Dersler_Akademisyenler_AkademisyenId",
                        column: x => x.AkademisyenId,
                        principalTable: "Akademisyenler",
                        principalColumn: "AkademisyenId");
                });

            migrationBuilder.CreateTable(
                name: "OgrenciDersler",
                columns: table => new
                {
                    OgrenciNo = table.Column<string>(type: "TEXT", nullable: false),
                    DersKodu = table.Column<string>(type: "TEXT", nullable: false),
                    NotlarJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OgrenciDersler", x => new { x.OgrenciNo, x.DersKodu });
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_Dersler_DersKodu",
                        column: x => x.DersKodu,
                        principalTable: "Dersler",
                        principalColumn: "Kod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OgrenciDersler_Ogrenciler_OgrenciNo",
                        column: x => x.OgrenciNo,
                        principalTable: "Ogrenciler",
                        principalColumn: "OgrenciNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_AkademisyenId",
                table: "Dersler",
                column: "AkademisyenId");

            migrationBuilder.CreateIndex(
                name: "IX_OgrenciDersler_DersKodu",
                table: "OgrenciDersler",
                column: "DersKodu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OgrenciDersler");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "Akademisyenler");
        }
    }
}
