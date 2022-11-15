using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/vendedores")]
    public class VendedoresController : ControllerBase
    {
        private readonly ILogger<VendedoresController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VendedoresController(ILogger<VendedoresController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<VendedorDTO>>> Get()
        {
            var vendedores = await context.Vendedores.ToListAsync();
            return mapper.Map<List<VendedorDTO>>(vendedores);

        }

        [HttpGet("listadoVendedores")]
        public async Task<ActionResult<List<VendedorDTO>>> GetUsuariosVendedores()
        {
            var usuarios = await context.Vendedores.OrderBy(x => x.Email).ToListAsync();
            return mapper.Map<List<VendedorDTO>>(usuarios);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VendedorDTO>> Get(int id)
        {
            var vendedor = await context.Vendedores.FirstOrDefaultAsync(x => x.Id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return mapper.Map<VendedorDTO>(vendedor);
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


        [HttpGet("filtrar")]
        public async Task<ActionResult<List<VendedorDTO>>> Filtrar([FromQuery] VendedoresFiltrarDTO vendedoresFiltrarDTO)
        {
            var vendedoresQueryable = context.Vendedores.AsQueryable();

            if (!string.IsNullOrEmpty(vendedoresFiltrarDTO.Nombres))
            {
                vendedoresQueryable = vendedoresQueryable.Where(x => x.Nombres.Contains(vendedoresFiltrarDTO.Nombres));
            }

            if (!string.IsNullOrEmpty(vendedoresFiltrarDTO.Apellidos))
            {
                vendedoresQueryable = vendedoresQueryable.Where(x => x.Apellidos.Contains(vendedoresFiltrarDTO.Apellidos));
            }

            if (vendedoresFiltrarDTO.StateVendedor)
            {
                vendedoresQueryable = vendedoresQueryable.Where(x => x.StateVendedor);
            }

            if (vendedoresFiltrarDTO.StateDomiciliario)
            {
                vendedoresQueryable = vendedoresQueryable.Where(x => x.StateDomiciliario);
            }

            var vendedores = await vendedoresQueryable.ToListAsync();
            return mapper.Map<List<VendedorDTO>>(vendedores);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VendedorCreacionDTO vendedorCreacionDTO)
        {
            var vendedor = mapper.Map<Vendedor>(vendedorCreacionDTO);
            context.Add(vendedor);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
