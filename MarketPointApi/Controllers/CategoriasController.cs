using AutoMapper;
using MarketPointApi.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriasController : ControllerBase
    {
        private readonly ILogger<CategoriasController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CategoriasController(ILogger<CategoriasController> logger,
            ApplicationDbContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var categorias = await context.Categorias.OrderBy(x => x.Nombre).ToListAsync();
            return mapper.Map<List<CategoriaDTO>>(categorias);

        }

    }
}
