using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/misVentas")]
    public class MisVentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MisVentasController(UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("ventas/{id:int}")]
        public async Task<ActionResult<List<MisComprasDTO>>> GetAllVendedores(int id)
        {
            var vendedoresQueryable = context.MisCompras.AsQueryable();
            if (id != 0)
            {
                vendedoresQueryable = vendedoresQueryable.Where(x => x.VendedorId == id);
            }

            var misCompras = await vendedoresQueryable.ToListAsync();
            return mapper.Map<List<MisComprasDTO>>(misCompras);

        }

    }
}
