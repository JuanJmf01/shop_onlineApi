using MarketPointApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketPointApi.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public CuentasController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("CrearCliente")]
        public async Task<ActionResult<RespuestaAutenticacion>> CrearCliente([FromBody] CredencialesUsuario credenciales)
        {
            var usuario = new IdentityUser {
                UserName = credenciales.Nombres,
                Email = credenciales.Email
            };
            //Para crear un usuario utilizando Identity utilizamos userManager
            var resultado = await userManager.CreateAsync(usuario, credenciales.Password);

            //Si fue exitoso retornamos el token
            if (resultado.Succeeded)
            {
                //Llamamos a la function que crea el token y le pasamos las credenciales de usuario
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login([FromBody] CredencialesUsuario credenciales)
        {
            var resultado = await signInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }

        }


        //Construccion de JWT (json web token) y claims
        //JWT: es un script encriptado formado por una cabecera, datos(claim) y la firma (llavejws)
        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credenciales)
        {
            //claim: Conjunto de datos confiables acerca del usuario
            var claims = new List<Claim>()
            {
                new Claim("email", credenciales.Email)
            };

            // Traemos el usuario para traer los claims de ese usuario en base de datos
            var usuario = await userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            //Le agregamos a la Lista<Claim> el claimsBD del usuario que traemos de BD
            claims.AddRange(claimsDB);

            // Llave secreta que estamos utilizando en el proveedor de configuracion. (La encriptamos)
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            //Tiempo de expiracion del Token va ser de una año
            var expiracion = DateTime.UtcNow.AddYears(1);

            //Armamos el token con los claims, su expiracion y las usuarioCreacionDTO 
            //Recordar que: JWT(Token) o (JSON WEB TOKEN): es un script encriptado formado por una cabecera, datos(claim) y la firma (llavejws)
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
               expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };
        }
    }
}
