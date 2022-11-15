using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using MarketPointApi.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string contenedor = "productos";

        public ProductosController(ApplicationDbContext context,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<LandingPageDTO>> Get()
        {
            var enOferta = true;

            var oferta = await context.Productos
                .Where(x => x.Oferta == enOferta)
                .ToListAsync();

            var noOferta = await context.Productos
                .Where(x => x.Oferta != enOferta)
                .ToListAsync();

            var resultado = new LandingPageDTO();
            resultado.Ofertas = mapper.Map<List<ProductoDTO>>(oferta);
            resultado.Productos = mapper.Map<List<ProductoDTO>>(noOferta);

            return resultado;
        }


        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await context.Productos
                //Primero mapeamos a ProductosCategorias, desps de esa tabla, mapeamos a Categoria
                //De esta manera podemos utilizar los datos de las tablas producto y categoria, gracias a ProductosCategoria
                .Include(x => x.ProductosCategorias).ThenInclude(x => x.Categoria)
                .Include(x => x.ProductosVendedores).ThenInclude(x => x.Vendedor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (producto == null) { return NotFound(); }

            var promedioVoto = 0.0;
            var usuarioVoto = 0;

            if (await context.Ratings.AnyAsync(x => x.ProductoId == id))
            {
                promedioVoto = await context.Ratings.Where(x => x.ProductoId == id)
                    .AverageAsync(x => x.Puntuacion);

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    var usuario = await userManager.FindByEmailAsync(email);
                    var usuarioId = usuario.Id;
                    var ratingDB = await context.Ratings
                        .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.ProductoId == id);

                    if(ratingDB != null)
                    {
                        usuarioVoto = ratingDB.Puntuacion;
                    }
                }
                
            }

            var dto = mapper.Map<ProductoDTO>(producto);
            dto.VotoUsuario = usuarioVoto;
            dto.PromedioVoto = promedioVoto;
            return dto;
        }

        [HttpGet("misProductos/{id:int}")]
        public async Task<ActionResult<LandingPageDTO>> MisProductos(int id)
        {
            var productosQueryable = context.Productos.AsQueryable();

            if(id != 0)
            {
                productosQueryable = productosQueryable
                    // Paso 1: vamos a ProductosVendedores
                    // Paso 2: De productosVendedores selecionamos todos aquellas filas que coninsidan con el id que nos llega
                    // Paso 3: Finalmente solo traemos las peliculas que en su Id coinsidan con alguno de los ids del paso dos
                    .Where(x => x.ProductosVendedores.Select(y => y.VendedorId)
                    .Contains(id));
            }
            var productos = await productosQueryable.ToListAsync();

            //Sepearamos las no ofertas de las ofertas para utilizarlas por aparte
            var enOferta = true;
            var oferta = productos
                .Where(x => x.Oferta == enOferta);

            var noOferta = productos
                .Where(x => x.Oferta != enOferta);

            var resultado = new LandingPageDTO();
            resultado.Ofertas = mapper.Map<List<ProductoDTO>>(oferta);
            resultado.Productos = mapper.Map<List<ProductoDTO>>(noOferta);

            return resultado;

        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<ProductoDTO>>> Filtrar([FromQuery] ProductosFiltrarDTO productosFiltrarDTO)
        {
            var productosQueryable = context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(productosFiltrarDTO.Nombre))
            {
                productosQueryable = productosQueryable.Where(x => x.Nombre.Contains(productosFiltrarDTO.Nombre));
            }

            if (productosFiltrarDTO.Oferta)
            {
                productosQueryable = productosQueryable.Where(x => x.Oferta);
            }

            if(productosFiltrarDTO.CategoriaId != 0)
            {
                productosQueryable = productosQueryable
                    //Paso 1: De la tabla categorias, seleccionamos todos aquellos isCategorias que conincidan
                    //        Con el idCategoria que nos llega del front
                    //Paso 2: Finalmente solo traemos los productos que contengan(Contains) ese idCategoria 
                    //        Que en el paso 1 seleccionamos
                    .Where(x => x.ProductosCategorias.Select(y => y.CategoriaId)
                    .Contains(productosFiltrarDTO.CategoriaId));

            }

            var productos = await productosQueryable.ToListAsync();
            return mapper.Map<List<ProductoDTO>>(productos);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductoCreacionDTO productoCreacionDTO)
        {
            var producto = mapper.Map<Producto>(productoCreacionDTO);

            if (productoCreacionDTO.ImagenProducto != null)
            {
                producto.ImagenProducto = await almacenadorArchivos.GuardarArchivo(contenedor, productoCreacionDTO.ImagenProducto);
            }

            context.Add(producto);
            await context.SaveChangesAsync();
            return NoContent();
        }
    
        //En este metodo (enpoind) vamos a llamar las categorias para mostrarlas en categorias seleccionadas en el front
        [HttpGet("PostGet")]
        public async Task<ActionResult<ProductosPostGetDTO>> PostGet()
        {
            var categorias = await context.Categorias.ToListAsync();
            
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);

            return new ProductosPostGetDTO() { Categorias = categoriasDTO };
        }

        //Metodo nos permite cargar (obtener) los datos de los productos
        [HttpGet("PutGet/{id:int}")]
        public async Task<ActionResult<ProductosPutGetDTO>> PutGet(int id)
        {
            var productoActionResult = await Get(id);
            if(productoActionResult.Result is NotFoundResult) { return NotFound();  }

            var producto = productoActionResult.Value;

            //Primero buscamos las categorias seleccionados que vienen del metodo Get que llamamos
            var categoriasSeleccionadas = producto.Categorias.Select(x => x.Id).ToList();  //6 y 7

            //Despues, buscamos en nuestra tabla Categorias en BD, aquellas categorias por id
            // que son diferentes (por id) al id de los categoriasSeleccionadas
            var categoriasNoSeleccionadas = await context.Categorias
                .Where(x => !categoriasSeleccionadas.Contains(x.Id))
                .ToListAsync();

            var categoriasNoSeleccionadasDTO = mapper.Map<List<CategoriaDTO>>(categoriasNoSeleccionadas);

            var respuesta = new ProductosPutGetDTO();
            respuesta.Producto = producto;
            respuesta.CategoriasSeleccionadas = producto.Categorias;
            respuesta.CategoriasNoSeleccionadas = categoriasNoSeleccionadasDTO;
            
            return respuesta;

        }


   
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductoCreacionDTO productoCreacionDTO)
        {
            //Utilizamos include de las demas tablas ya que ciertos datos vienen de aquellas tablas
            //por lo que nos toca mapear a Categorias desde ta tabla Productos, por medio de
            // la tabla resultante de dos tablas muchos a muchos
            var producto = await context.Productos
                .Include(x => x.ProductosCategorias)
                .Include(x => x.ProductosVendedores)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(producto == null)
            {
                return NotFound();
            }

            producto = mapper.Map(productoCreacionDTO, producto);

            if (productoCreacionDTO.ImagenProducto != null)
            {
                producto.ImagenProducto = await almacenadorArchivos.EditarArchivo(contenedor, productoCreacionDTO.ImagenProducto, producto.ImagenProducto);
            }

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            if(producto == null)
            {
                return NotFound();
            }

            context.Remove(producto);
            await context.SaveChangesAsync();
            await almacenadorArchivos.BorrarArchivo(producto.ImagenProducto, contenedor);
            return NoContent();

        }



    }
}
