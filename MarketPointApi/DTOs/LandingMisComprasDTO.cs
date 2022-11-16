namespace MarketPointApi.DTOs
{
    public class LandingMisComprasDTO
    {

        public List<ProductoDTO> Compras { get; set; }
        public List<ProductoDTO> enProceso { get; set; }


        //public List<ProductoIdDTO> Ids { get; set; }
        
    }
}
