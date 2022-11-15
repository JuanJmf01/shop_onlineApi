using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.Entidades
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Puntuacion { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }

    }
}
