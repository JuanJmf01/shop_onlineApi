namespace MarketPointApi.Entidades
{
    public class ProductosVendedores
    {
        public int ProductoId { get; set; }
        public int VendedorId { get; set; }

        //Propiedades de navegacion para poder ir de una entidad a otra
        public Producto Producto { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}
