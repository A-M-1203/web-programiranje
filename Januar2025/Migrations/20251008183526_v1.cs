using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Januar2025.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Kilometraza = table.Column<int>(type: "int", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    BrojSedista = table.Column<int>(type: "int", nullable: false),
                    CenaPoDanu = table.Column<int>(type: "int", nullable: false),
                    TrenutnoIznajmljen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BrojVozacke = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iznajmljivanja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutomobilId = table.Column<int>(type: "int", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    BrojDana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iznajmljivanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Iznajmljivanja_Automobili_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "Automobili",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Iznajmljivanja_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanja_AutomobilId",
                table: "Iznajmljivanja",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_Iznajmljivanja_KorisnikId",
                table: "Iznajmljivanja",
                column: "KorisnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iznajmljivanja");

            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Korisnici");
        }
    }
}
