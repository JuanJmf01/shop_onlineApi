namespace MarketPointApi.DTOs
{
    public class MedioDePagoCreacionDTO
    {
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public string Nombre { get; set; }
        public IFormFile imagenMedioPago { get; set; }

    }
}
