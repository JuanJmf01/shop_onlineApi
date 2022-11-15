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
        public bool EsCliente { get; set; }
    }
}
