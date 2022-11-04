﻿using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class CredencialesUsuario
    {
        public string Nombres { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
