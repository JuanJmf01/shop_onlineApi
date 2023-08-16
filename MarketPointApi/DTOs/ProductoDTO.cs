using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public bool Oferta { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public int CantidadDisponible { get; set; }
        public bool Disponible { get; set; }
        public string ImagenProducto { get; set; }

        //Mappear entre las tablas categorias y vendedores
        public List<CategoriaDTO> Categorias { get; set; }
        public List<ProductoVendedorDTO> Vendedores  { get; set; }


        public int VotoUsuario { get; set; }
        public double PromedioVoto { get; set; }



    }
}
