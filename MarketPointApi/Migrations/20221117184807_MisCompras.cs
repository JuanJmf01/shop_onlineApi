using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPointApi.Migrations
{
    public partial class MisCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MisCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vendido = table.Column<bool>(type: "bit", nullable: false),
                    EsCliente = table.Column<bool>(type: "bit", nullable: false),
                    ImagenComprobante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MisCompras_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MisCompras_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MisCompras_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MisCompras_ClienteId",
                table: "MisCompras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MisCompras_ProductoId",
                table: "MisCompras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MisCompras_VendedorId",
                table: "MisCompras",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MisCompras");
        }
    }
}
