using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //Esto nos sirve para traer de una categoria, todos sus productos
        public List<ProductosCategorias> ProductosCategorias { get; set; }
    }
}
