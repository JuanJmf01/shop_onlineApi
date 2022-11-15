using MarketPointApi.Entidades;

namespace MarketPointApi.DTOs
{
    public class MisComprasCreacionDTO
    {
        public bool Vendido { get; set; }
        public bool EnProceso { get; set; }
        public int UsuarioId { get; set; }
        public int VendedorId { get; set; }
        public int ProductoId { get; set; }
    }
}
