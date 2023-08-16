using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class VendedorDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public long NumeroCelular { get; set; }
        public string NombreNegocio { get; set; }
        public string DescripcionNegocio { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public bool StateVendedor { get; set; }
        public bool StateDomiciliario { get; set; }
        public string Password { get; set; }
    }
}
