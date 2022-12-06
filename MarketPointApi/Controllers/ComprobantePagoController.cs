/*
 using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/comprobantePago")]
    public class ComprobantePagoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "productos";


        public ComprobantePagoController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<ComprobantePagoDTO>>> GetAllVendedores(int id)
        {
            var prueba = await context.ComprobantesDePago.FirstOrDefaultAsync(x => x. == id).ToListAsync();
            var second = prueba.Select(x => new { x.Id, x.imagenMedioPago, x.Nombre }).ToList()
                .Select(x => new MedioDePago() { Id = x.Id, imagenMedioPago = x.imagenMedioPago, Nombre = x.Nombre }).ToList();

            return mapper.Map<List<ComprobantePagoDTO>>(second);

        }



        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ComprobantePagoCreacionDTO comprobantePagoCreacionDTO)
        {
            var comprobante = mapper.Map<ComprobantePago>(comprobantePagoCreacionDTO);

            if (comprobantePagoCreacionDTO.ImagenComprobante != null)
            {
                comprobante.ImagenComprobante = await almacenadorArchivos.GuardarArchivo(contenedor, comprobantePagoCreacionDTO.ImagenComprobante);
            }

            context.Add(comprobante);
            await context.SaveChangesAsync();
            return NoContent();
        }



    }
}

 */