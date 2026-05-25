using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerAutoMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telefon = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    KrajPochodzenia = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    HasloHash = table.Column<string>(type: "TEXT", nullable: false),
                    TokenApi = table.Column<string>(type: "TEXT", nullable: false),
                    CzyAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModeleSamochodow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarkaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Rok = table.Column<int>(type: "INTEGER", nullable: false),
                    Pojemnosc = table.Column<int>(type: "INTEGER", nullable: false),
                    HorsePower = table.Column<int>(type: "INTEGER", nullable: false),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false),
                    Przebieg = table.Column<int>(type: "INTEGER", nullable: false),
                    Kolor = table.Column<string>(type: "TEXT", nullable: false),
                    CzySprzedany = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeleSamochodow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModeleSamochodow_Marki_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Marki",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transakcje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KlientId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModelSamochoduId = table.Column<int>(type: "INTEGER", nullable: true),
                    DataTransakcji = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CenaSprzedazy = table.Column<decimal>(type: "TEXT", nullable: false),
                    FormaPlatnosci = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcje_Klienci_KlientId",
                        column: x => x.KlientId,
                        principalTable: "Klienci",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transakcje_ModeleSamochodow_ModelSamochoduId",
                        column: x => x.ModelSamochoduId,
                        principalTable: "ModeleSamochodow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wyposazenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModelSamochoduId = table.Column<int>(type: "INTEGER", nullable: true),
                    Klimatyzacja = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nawigacja = table.Column<bool>(type: "INTEGER", nullable: false),
                    SkorzanaTapicerka = table.Column<bool>(type: "INTEGER", nullable: false),
                    KameraCofania = table.Column<bool>(type: "INTEGER", nullable: false),
                    CzujnikiParkowania = table.Column<bool>(type: "INTEGER", nullable: false),
                    AppleCarPlay = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wyposazenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wyposazenia_ModeleSamochodow_ModelSamochoduId",
                        column: x => x.ModelSamochoduId,
                        principalTable: "ModeleSamochodow",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModeleSamochodow_MarkaId",
                table: "ModeleSamochodow",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcje_KlientId",
                table: "Transakcje",
                column: "KlientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcje_ModelSamochoduId",
                table: "Transakcje",
                column: "ModelSamochoduId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wyposazenia_ModelSamochoduId",
                table: "Wyposazenia",
                column: "ModelSamochoduId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transakcje");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");

            migrationBuilder.DropTable(
                name: "Wyposazenia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "ModeleSamochodow");

            migrationBuilder.DropTable(
                name: "Marki");
        }
    }
}
