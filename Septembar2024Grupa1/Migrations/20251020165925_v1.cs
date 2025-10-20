using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Septembar2024Grupa1.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stanovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeVlasnika = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Povrsina = table.Column<int>(type: "int", nullable: false),
                    BrojClanova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StanId = table.Column<int>(type: "int", nullable: false),
                    MesecIzdavanja = table.Column<int>(type: "int", nullable: false),
                    CenaStruje = table.Column<int>(type: "int", nullable: false),
                    CenaVode = table.Column<int>(type: "int", nullable: false),
                    CenaKomunalija = table.Column<int>(type: "int", nullable: false),
                    Placen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Racuni_Stanovi_StanId",
                        column: x => x.StanId,
                        principalTable: "Stanovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_StanId",
                table: "Racuni",
                column: "StanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Stanovi");
        }
    }
}
