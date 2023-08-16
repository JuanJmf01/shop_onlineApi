using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class ProductoCreacionDTO
    {
        public bool Oferta { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public int CantidadDisponible { get; set; } 
        public bool Disponible { get; set; }
        public IFormFile ImagenProducto { get; set; }  //IFormFile: para tivo archivos

        //Utilizamos ModelBinder (personalizado (TypeBinder)) para recibir el listado de  ids categorias de ProductosCategorias 
        // para el caso en que necesitemos traer por ej productos por categoria o al contrario
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> CategoriasIds { get; set; }

        // En este caso, no necesitamos un arreglo ya que solo nos llega un Id
        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> VendedoresIds { get; set; }



    }
}
