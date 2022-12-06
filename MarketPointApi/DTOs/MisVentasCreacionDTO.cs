using MarketPointApi.Entidades;

namespace MarketPointApi.DTOs
{
    public class MisVentasCreacionDTO
    {
        public int Id { get; set; }
        public IFormFile imagenComprobante { get; set; }
    }
}
