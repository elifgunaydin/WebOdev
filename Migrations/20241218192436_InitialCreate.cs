using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KuaforWebSitesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    CalisanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanAd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalisanSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalisanMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalisanTelefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalisanSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.CalisanID);
                });

            migrationBuilder.CreateTable(
                name: "Gunler",
                columns: table => new
                {
                    GunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GunAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gunler", x => x.GunID);
                });

            migrationBuilder.CreateTable(
                name: "HizmetKategoriler",
                columns: table => new
                {
                    HizmetKategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HizmetKategoriler", x => x.HizmetKategoriID);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    MusteriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriAd = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MusteriSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusteriMail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MusteriTelefon = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MusteriSifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.MusteriID);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "CalisanGunler",
                columns: table => new
                {
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    GunID = table.Column<int>(type: "int", nullable: false),
                    CalisanGunID = table.Column<int>(type: "int", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanGunler", x => new { x.CalisanID, x.GunID });
                    table.ForeignKey(
                        name: "FK_CalisanGunler_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanGunler_Gunler_GunID",
                        column: x => x.GunID,
                        principalTable: "Gunler",
                        principalColumn: "GunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hizmetler",
                columns: table => new
                {
                    HizmetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sure = table.Column<TimeSpan>(type: "time", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HizmetKategoriID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.HizmetID);
                    table.ForeignKey(
                        name: "FK_Hizmetler_HizmetKategoriler_HizmetKategoriID",
                        column: x => x.HizmetKategoriID,
                        principalTable: "HizmetKategoriler",
                        principalColumn: "HizmetKategoriID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MusteriRoller",
                columns: table => new
                {
                    MusteriRolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    RolID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriRoller", x => x.MusteriRolID);
                    table.ForeignKey(
                        name: "FK_MusteriRoller_Musteriler_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriRoller_Roller_RolID",
                        column: x => x.RolID,
                        principalTable: "Roller",
                        principalColumn: "RolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanHizmetler",
                columns: table => new
                {
                    CalisanHizmetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    HizmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanHizmetler", x => x.CalisanHizmetID);
                    table.ForeignKey(
                        name: "FK_CalisanHizmetler_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanHizmetler_Hizmetler_HizmetID",
                        column: x => x.HizmetID,
                        principalTable: "Hizmetler",
                        principalColumn: "HizmetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    RandevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HizmetID = table.Column<int>(type: "int", nullable: false),
                    CalisanID = table.Column<int>(type: "int", nullable: false),
                    MusteriID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Saat = table.Column<TimeSpan>(type: "time", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.RandevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_CalisanID",
                        column: x => x.CalisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "CalisanID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Hizmetler_HizmetID",
                        column: x => x.HizmetID,
                        principalTable: "Hizmetler",
                        principalColumn: "HizmetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Musteriler_MusteriID",
                        column: x => x.MusteriID,
                        principalTable: "Musteriler",
                        principalColumn: "MusteriID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Calisanlar",
                columns: new[] { "CalisanID", "CalisanAd", "CalisanMail", "CalisanSifre", "CalisanSoyad", "CalisanTelefon" },
                values: new object[,]
                {
                    { 1, "Dilan", "dilan.kaya@shineLab.com", "Dilan123", "Kaya", "05395456751" },
                    { 2, "Elif", "elif.yilmaz@shineLab.com", "Elif123", "Yılmaz", "05395456752" },
                    { 3, "Şeyma", "seyma.cetin@shineLab.com", "Seyma123", "Çetin", "05395456753" },
                    { 4, "Ece", "ece.tuncer@shineLab.com", "Ece123", "Tuncer", "05395456754" },
                    { 5, "Aslı", "asli.sahin@shineLab.com", "ShineLaB", "Şahin", "05395456755" },
                    { 6, "Ceyda", "ceyda.ozdemir@shineLab.com", "Ceyda123", "Özdemir", "05395456756" },
                    { 7, "Nisan", "nisan.kaya@shineLab.com", "Nisan123", "Kaya", "05395456757" }
                });

            migrationBuilder.InsertData(
                table: "Gunler",
                columns: new[] { "GunID", "GunAdi" },
                values: new object[,]
                {
                    { 1, "Pazartesi" },
                    { 2, "Salı" },
                    { 3, "Çarşamba" },
                    { 4, "Perşembe" },
                    { 5, "Cuma" },
                    { 6, "Cumartesi" },
                    { 7, "Pazar" }
                });

            migrationBuilder.InsertData(
                table: "HizmetKategoriler",
                columns: new[] { "HizmetKategoriID", "KategoriAdi" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi" },
                    { 2, "Saç Bakımı" },
                    { 3, "Manikür" },
                    { 4, "Pedikür" },
                    { 5, "Gelin" }
                });

            migrationBuilder.InsertData(
                table: "CalisanGunler",
                columns: new[] { "CalisanID", "GunID", "BaslangicSaati", "BitisSaati", "CalisanGunID" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 1 },
                    { 1, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 2 },
                    { 2, 2, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 3 },
                    { 2, 3, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 4 },
                    { 2, 4, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 5 },
                    { 2, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 6 },
                    { 2, 6, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 7 },
                    { 3, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 8 },
                    { 3, 3, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 9 },
                    { 3, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 10 },
                    { 3, 6, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 11 },
                    { 4, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 12 },
                    { 4, 2, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 13 },
                    { 4, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 14 },
                    { 4, 6, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 15 },
                    { 5, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 16 },
                    { 5, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 17 },
                    { 5, 7, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 18 },
                    { 6, 2, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 19 },
                    { 6, 3, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 20 },
                    { 6, 5, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 21 },
                    { 6, 6, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 22 },
                    { 6, 7, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 23 },
                    { 7, 1, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 24 },
                    { 7, 3, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 25 },
                    { 7, 4, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 26 },
                    { 7, 7, new TimeSpan(0, 0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 0), 27 }
                });

            migrationBuilder.InsertData(
                table: "Hizmetler",
                columns: new[] { "HizmetID", "Fiyat", "HizmetAdi", "HizmetKategoriID", "Sure" },
                values: new object[,]
                {
                    { 1, 400m, "Fön", 2, new TimeSpan(0, 1, 0, 0, 0) },
                    { 2, 800m, "Maşa", 2, new TimeSpan(0, 1, 0, 0, 0) },
                    { 3, 1000m, "Örgü", 2, new TimeSpan(0, 1, 0, 0, 0) },
                    { 4, 1200m, "Topuz", 2, new TimeSpan(0, 1, 0, 0, 0) },
                    { 5, 1300m, "Saç Kesim", 2, new TimeSpan(0, 1, 0, 0, 0) },
                    { 6, 2500m, "Cila", 1, new TimeSpan(0, 0, 45, 0, 0) },
                    { 7, 1250m, "Dip Boya", 1, new TimeSpan(0, 1, 0, 0, 0) },
                    { 8, 1750m, "Transparan Boya", 1, new TimeSpan(0, 1, 0, 0, 0) },
                    { 9, 2500m, "Bütün Boya", 1, new TimeSpan(0, 2, 0, 0, 0) },
                    { 10, 5500m, "Brushlight", 1, new TimeSpan(0, 3, 0, 0, 0) },
                    { 11, 6000m, "Highlight", 1, new TimeSpan(0, 5, 0, 0, 0) },
                    { 12, 8000m, "Röfle", 1, new TimeSpan(0, 5, 0, 0, 0) },
                    { 13, 4000m, "Sakinleştirici Bakım", 1, new TimeSpan(0, 1, 30, 0, 0) },
                    { 14, 6000m, "Düzleştirici Bakım", 1, new TimeSpan(0, 2, 30, 0, 0) },
                    { 15, 6000m, "Gelin Saçı", 5, new TimeSpan(0, 3, 0, 0, 0) },
                    { 16, 6000m, "Gelin Makyajı", 5, new TimeSpan(0, 1, 30, 0, 0) },
                    { 17, 750m, "Manikür", 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 18, 750m, "Pedikür", 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 19, 550m, "El Kalıcı Oje", 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 20, 750m, "Ayak Kalıcı Oje", 3, new TimeSpan(0, 0, 45, 0, 0) },
                    { 21, 2500m, "Profesyonel Cilt Bakımı", 4, new TimeSpan(0, 1, 30, 0, 0) },
                    { 22, 400m, "Kaş Alımı", 4, new TimeSpan(0, 0, 20, 0, 0) },
                    { 23, 1250m, "Kirpik Lifting", 4, new TimeSpan(0, 0, 45, 0, 0) },
                    { 24, 1500m, "Karbon Peeling", 4, new TimeSpan(0, 0, 45, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "CalisanHizmetler",
                columns: new[] { "CalisanHizmetID", "CalisanID", "HizmetID" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 1, 6 },
                    { 7, 1, 7 },
                    { 8, 1, 8 },
                    { 9, 1, 9 },
                    { 10, 1, 10 },
                    { 11, 1, 11 },
                    { 12, 1, 12 },
                    { 13, 1, 13 },
                    { 14, 1, 14 },
                    { 15, 1, 15 },
                    { 16, 1, 16 },
                    { 17, 2, 1 },
                    { 18, 2, 2 },
                    { 19, 2, 3 },
                    { 20, 2, 4 },
                    { 21, 2, 5 },
                    { 22, 2, 6 },
                    { 23, 2, 7 },
                    { 24, 2, 8 },
                    { 25, 2, 9 },
                    { 26, 2, 10 },
                    { 27, 2, 11 },
                    { 28, 2, 12 },
                    { 29, 2, 13 },
                    { 30, 2, 14 },
                    { 31, 2, 15 },
                    { 32, 2, 16 },
                    { 33, 3, 1 },
                    { 34, 3, 2 },
                    { 35, 3, 3 },
                    { 36, 3, 4 },
                    { 37, 3, 5 },
                    { 38, 3, 6 },
                    { 39, 3, 7 },
                    { 40, 3, 8 },
                    { 41, 3, 9 },
                    { 42, 3, 10 },
                    { 43, 3, 11 },
                    { 44, 3, 12 },
                    { 45, 3, 13 },
                    { 46, 3, 14 },
                    { 47, 4, 1 },
                    { 48, 4, 2 },
                    { 49, 4, 3 },
                    { 50, 4, 4 },
                    { 51, 4, 5 },
                    { 52, 4, 6 },
                    { 53, 4, 7 },
                    { 54, 4, 8 },
                    { 55, 4, 9 },
                    { 56, 4, 10 },
                    { 57, 4, 11 },
                    { 58, 4, 12 },
                    { 59, 4, 13 },
                    { 60, 4, 14 },
                    { 61, 4, 15 },
                    { 62, 4, 16 },
                    { 63, 4, 17 },
                    { 64, 4, 18 },
                    { 65, 4, 19 },
                    { 66, 4, 20 },
                    { 67, 4, 21 },
                    { 68, 4, 22 },
                    { 69, 4, 23 },
                    { 70, 4, 24 },
                    { 71, 5, 1 },
                    { 72, 5, 2 },
                    { 73, 5, 3 },
                    { 74, 5, 4 },
                    { 75, 5, 5 },
                    { 76, 6, 17 },
                    { 77, 6, 18 },
                    { 78, 6, 19 },
                    { 79, 6, 20 },
                    { 80, 6, 21 },
                    { 81, 6, 22 },
                    { 82, 6, 23 },
                    { 83, 6, 24 },
                    { 84, 7, 17 },
                    { 85, 7, 18 },
                    { 86, 7, 19 },
                    { 87, 7, 20 },
                    { 88, 7, 21 },
                    { 89, 7, 22 },
                    { 90, 7, 23 },
                    { 91, 7, 24 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanGunler_GunID",
                table: "CalisanGunler",
                column: "GunID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanHizmetler_CalisanID",
                table: "CalisanHizmetler",
                column: "CalisanID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanHizmetler_HizmetID",
                table: "CalisanHizmetler",
                column: "HizmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_HizmetKategoriID",
                table: "Hizmetler",
                column: "HizmetKategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_MusteriMail",
                table: "Musteriler",
                column: "MusteriMail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_MusteriTelefon",
                table: "Musteriler",
                column: "MusteriTelefon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusteriRoller_MusteriID",
                table: "MusteriRoller",
                column: "MusteriID");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriRoller_RolID",
                table: "MusteriRoller",
                column: "RolID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_CalisanID",
                table: "Randevular",
                column: "CalisanID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_HizmetID",
                table: "Randevular",
                column: "HizmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_MusteriID",
                table: "Randevular",
                column: "MusteriID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanGunler");

            migrationBuilder.DropTable(
                name: "CalisanHizmetler");

            migrationBuilder.DropTable(
                name: "MusteriRoller");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Gunler");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "HizmetKategoriler");
        }
    }
}
