using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.Entidades
{
    //Entidad especial ya que tiene relaciones con otras entidades
    //Por ejemplo un producto puede tener muchas categorias
    //Y una categoria puede tener muchos productos VER ProductosCategorias.cs
    //Pasa lo mismo con vendedores 
    public class Producto
    {
        public int Id { get; set; }
        public bool Oferta { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public int CantidadDisponible { get; set; } 
        public bool Disponible { get; set; }
        public string ImagenProducto { get; set; }
        //Adicionales: ejm: si traemos un producto y queremos ver todas sus categorias
        //             ejm: si traemos una categoria y queremos ver todos sus productos
        //             De igual manera para vendedores-Producto
        public List<ProductosCategorias> ProductosCategorias { get; set; }
        public List<ProductosVendedores> ProductosVendedores { get; set; }
    }
}
