using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.Entidades
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public long NumeroCelular { get; set; }
        public string NombreNegocio { get; set; }
        public string DescripcionNegocio { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public bool StateVendedor { get; set; }
        public bool StateDomiciliario { get; set; }
        [Required]
        public string Password { get; set; }
        //Esto nos sirve para traer de una vendedor, todos sus productos
        public List<ProductosVendedores> ProductosVendedores { get; set; }

    }
}
