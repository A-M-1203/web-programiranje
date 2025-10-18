using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oktobar2025Grupa1.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodavnice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Kapacitet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sastojci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Debljina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sastojci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hamburgeri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Prodat = table.Column<bool>(type: "bit", nullable: false),
                    ProdavnicaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hamburgeri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hamburgeri_Prodavnice_ProdavnicaId",
                        column: x => x.ProdavnicaId,
                        principalTable: "Prodavnice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HamburgerSastojak",
                columns: table => new
                {
                    HamburgeriId = table.Column<int>(type: "int", nullable: false),
                    SastojciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HamburgerSastojak", x => new { x.HamburgeriId, x.SastojciId });
                    table.ForeignKey(
                        name: "FK_HamburgerSastojak_Hamburgeri_HamburgeriId",
                        column: x => x.HamburgeriId,
                        principalTable: "Hamburgeri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HamburgerSastojak_Sastojci_SastojciId",
                        column: x => x.SastojciId,
                        principalTable: "Sastojci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hamburgeri_ProdavnicaId",
                table: "Hamburgeri",
                column: "ProdavnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_HamburgerSastojak_SastojciId",
                table: "HamburgerSastojak",
                column: "SastojciId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HamburgerSastojak");

            migrationBuilder.DropTable(
                name: "Hamburgeri");

            migrationBuilder.DropTable(
                name: "Sastojci");

            migrationBuilder.DropTable(
                name: "Prodavnice");
        }
    }
}
