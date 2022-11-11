using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public long NumeroCelular { get; set; }
        public bool StateDomiciliario { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
