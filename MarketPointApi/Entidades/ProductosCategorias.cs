namespace MarketPointApi.Entidades
{
    //Entidad especial VER producto.cs
    //utilizamos 
    public class ProductosCategorias
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        //Propiedades de navegacion para ir de una entidad a otra
        //En este caso: Producto a entidad Vendedor y viceversa
        public Producto Producto { get; set; } 
        public Categoria Categoria { get; set; }


    }
}
