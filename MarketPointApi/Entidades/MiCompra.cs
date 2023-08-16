using Microsoft.AspNetCore.Identity;

namespace MarketPointApi.Entidades
{
    public class MiCompra
    {
        public int Id { get; set; }
        public bool Vendido { get; set; }
        public bool EsCliente { get; set; }
        public string ImagenComprobante { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

    }
}
