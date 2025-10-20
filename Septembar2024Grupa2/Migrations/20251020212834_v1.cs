using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Septembar2024Grupa2.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kupovine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UkupnaCena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupovine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    KratakOpis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DuziOpis = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Kategorija = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProizvodiKupovine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProizvodId = table.Column<int>(type: "int", nullable: false),
                    KupovinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProizvodiKupovine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProizvodiKupovine_Kupovine_KupovinaId",
                        column: x => x.KupovinaId,
                        principalTable: "Kupovine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProizvodiKupovine_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodiKupovine_KupovinaId",
                table: "ProizvodiKupovine",
                column: "KupovinaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodiKupovine_ProizvodId",
                table: "ProizvodiKupovine",
                column: "ProizvodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProizvodiKupovine");

            migrationBuilder.DropTable(
                name: "Kupovine");

            migrationBuilder.DropTable(
                name: "Proizvodi");
        }
    }
}
