namespace MarketPointApi.Entidades
{
    public class MedioDePago
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string imagenMedioPago { get; set; }
        public int ClienteId { get; set; }
        public Usuario Cliente { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

    }
}
