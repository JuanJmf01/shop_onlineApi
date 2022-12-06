using MarketPointApi.Entidades;

namespace MarketPointApi.DTOs
{
    public class MisComprasDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int ProductoId { get; set; }
        public bool Vendido { get; set; }
        public string imagenComprobante { get; set; }
        public bool EsCliente { get; set; }

        public int IdPro { get; set; }
        public bool Oferta { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public bool Disponible { get; set; }
        public string ImagenProducto { get; set; }
        public VendedorDTO vendedor { get; set; }
        public UsuarioDTO usuario { get; set; }

        //Mappear entre las tablas categorias y vendedores
        public List<CategoriaDTO> Categorias { get; set; }
    }
}
