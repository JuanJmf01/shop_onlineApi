using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class RatingDTO
    {
        public int ProductoId { get; set; }
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}
