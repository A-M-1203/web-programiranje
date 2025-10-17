using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oktobar2025Grupa2.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProducentskeKuce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducentskeKuce", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Kategorija = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ProsecnaOcena = table.Column<double>(type: "float", nullable: false),
                    BrojOcena = table.Column<int>(type: "int", nullable: false),
                    ProducentskaKucaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filmovi_ProducentskeKuce_ProducentskaKucaId",
                        column: x => x.ProducentskaKucaId,
                        principalTable: "ProducentskeKuce",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmovi_ProducentskaKucaId",
                table: "Filmovi",
                column: "ProducentskaKucaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "ProducentskeKuce");
        }
    }
}
