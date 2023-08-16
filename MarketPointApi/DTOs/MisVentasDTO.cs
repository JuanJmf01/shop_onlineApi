using MarketPointApi.Entidades;

namespace MarketPointApi.DTOs
{
    public class MisVentasDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int ProductoId { get; set; }
        public int total { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public string imagenComprobante { get; set; }
        public bool esCliente { get; set; }
        public string NombreVendedor { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public string ImagenProducto { get; set; }

        public VendedorDTO vendedor { get; set; }
        public UsuarioDTO usuario { get; set; }

    }
}
