using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs.NewFolder
{
    public class CredencialesInicioSesion
    {
        public string Nombres { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
