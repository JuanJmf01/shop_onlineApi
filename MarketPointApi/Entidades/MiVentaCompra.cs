namespace MarketPointApi.Entidades
{
    public class MiVentaCompra
    {
        public int Id { get; set; }
        public int total { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public string imagenComprobante { get; set; }
        public bool esCliente { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
