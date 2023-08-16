﻿using System.ComponentModel.DataAnnotations;

namespace MarketPointApi.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public long NumeroCelular { get; set; }
        public bool StateDomiciliario { get; set; }
        public string Password { get; set; }


    }
}
