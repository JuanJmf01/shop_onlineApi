namespace MarketPointApi.DTOs
{
    public class ProductosPutGetDTO
    {
        public ProductoDTO Producto { get; set; }
        public List<CategoriaDTO> CategoriasSeleccionadas { get; set; }
        public List<CategoriaDTO> CategoriasNoSeleccionadas { get; set; }
    }
}
