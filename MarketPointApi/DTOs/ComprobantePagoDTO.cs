using MarketPointApi.Entidades;

namespace MarketPointApi.DTOs
{
    public class ComprobantePagoDTO
    {
        public int Id { get; set; }
        public string ImagenComprobante { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int ProductoId { get; set; }
    }
}
