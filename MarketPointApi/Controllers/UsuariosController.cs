using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ILogger<UsuariosController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var cliente = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();  
            }

            return mapper.Map<UsuarioDTO>(cliente);
        }

        [HttpGet("{Email}")]
        public async Task<ActionResult<UsuarioDTO>> GetClientes(string Email)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Email == Email);
            if (usuario == null)
            {
                return NotFound();
            }

            return mapper.Map<UsuarioDTO>(usuario);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            context.Add(usuario);
            await context.SaveChangesAsync();
            return NoContent();
        }
      


        [HttpGet("filtrar")]
        public async Task<ActionResult<List<UsuarioDTO>>> Filtrar([FromQuery] UsuariosFiltrarDTO usuariosFiltrarDTO)
        {
            var domiciliariosQueryable = context.Usuarios.AsQueryable();

            if (!string.IsNullOrEmpty(usuariosFiltrarDTO.Nombres))
            {
                domiciliariosQueryable = domiciliariosQueryable.Where(x => x.Nombres.Contains(usuariosFiltrarDTO.Nombres));
            }

            if (!string.IsNullOrEmpty(usuariosFiltrarDTO.Apellidos))
            {
                domiciliariosQueryable = domiciliariosQueryable.Where(x => x.Apellidos.Contains(usuariosFiltrarDTO.Apellidos));
            }

            if (usuariosFiltrarDTO.StateDomiciliario)
            {
                domiciliariosQueryable = domiciliariosQueryable.Where(x => x.StateDomiciliario);
            }

            var domiciliarios = await domiciliariosQueryable.ToListAsync();
            return mapper.Map<List<UsuarioDTO>>(domiciliarios);

        }


    }
}
