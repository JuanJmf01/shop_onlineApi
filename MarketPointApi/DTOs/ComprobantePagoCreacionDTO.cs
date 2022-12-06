namespace MarketPointApi.DTOs
{
    public class ComprobantePagoCreacionDTO
    {
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public int ProductoId { get; set; }
        public IFormFile ImagenComprobante { get; set; }
    }
}
