using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPointApi.Migrations
{
    public partial class MisMisVentasCompras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MisVentasCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    imagenComprobante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    esCliente = table.Column<bool>(type: "bit", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    VendedorId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisVentasCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MisVentasCompras_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MisVentasCompras_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MisVentasCompras_Vendedores_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MisVentasCompras_ClienteId",
                table: "MisVentasCompras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MisVentasCompras_ProductoId",
                table: "MisVentasCompras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_MisVentasCompras_VendedorId",
                table: "MisVentasCompras",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MisVentasCompras");
        }
    }
}
