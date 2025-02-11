using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api1.Migrations
{
    /// <inheritdoc />
    public partial class AddRefrescosOrdenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Refrescos",
                columns: table => new
                {
                    idRefresco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tamano = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Refresco__B25E6C8BBD60C3A4", x => x.idRefresco);
                });

            migrationBuilder.CreateTable(
                name: "RefrescosOrdenes",
                columns: table => new
                {
                    idOrden = table.Column<int>(type: "int", nullable: false),
                    idRefresco = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefrescosOrdene", x => new { x.idOrden, x.idRefresco });
                    table.ForeignKey(
                        name: "FK_RefrescosOrdene_Ordenes",
                        column: x => x.idOrden,
                        principalTable: "Ordenes",
                        principalColumn: "idOrden");
                    table.ForeignKey(
                        name: "FK_RefrescosOrdene_Refrescos",
                        column: x => x.idRefresco,
                        principalTable: "Refrescos",
                        principalColumn: "idRefresco");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefrescosOrdenes_idRefresco",
                table: "RefrescosOrdenes",
                column: "idRefresco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefrescosOrdenes");

            migrationBuilder.DropTable(
                name: "Refrescos");
        }
    }
}
