using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/mediosDePago")]
    public class MediosDePagoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "productos";

        public MediosDePagoController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<MedioDePagoDTO>>> GetAllVendedores(int id)
        {
            var prueba = await context.MediosDePago.Where(x => x.VendedorId == id).ToListAsync();
            var second = prueba.Select(x => new { x.Id, x.imagenMedioPago, x.Nombre }).ToList()
                .Select(x => new MedioDePago() { Id = x.Id, imagenMedioPago = x.imagenMedioPago, Nombre = x.Nombre}).ToList();

            return mapper.Map<List<MedioDePagoDTO>>(second);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MedioDePagoCreacionDTO medioDePagoCreacionDTO)
        {
            var mdPago = mapper.Map<MedioDePago>(medioDePagoCreacionDTO);

            if (medioDePagoCreacionDTO.imagenMedioPago != null)
            {
                mdPago.imagenMedioPago = await almacenadorArchivos.GuardarArchivo(contenedor, medioDePagoCreacionDTO.imagenMedioPago);
            }

            context.Add(mdPago);
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var medioPago = await context.MediosDePago.FirstOrDefaultAsync(x => x.Id == id);
            if (medioPago == null)
            {
                return NotFound();
            }

            context.Remove(medioPago);
            await context.SaveChangesAsync();
            await almacenadorArchivos.BorrarArchivo(medioPago.imagenMedioPago, contenedor);
            return NoContent();

        }
    }
}