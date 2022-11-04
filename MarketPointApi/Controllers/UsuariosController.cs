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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            context.Add(usuario);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{Email}")]
        public async Task<ActionResult<VendedorDTO>> GetVendedores(string Email)
        {
            var usuario = await context.Vendedores.FirstOrDefaultAsync(x => x.Email == Email);
            if (usuario == null)
            {
                return NotFound();
            }

            return mapper.Map<VendedorDTO>(usuario);

        }

        

    }
}
