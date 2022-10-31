using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class UsuarioCreacionDTO
    {
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public long NumeroCelular { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
