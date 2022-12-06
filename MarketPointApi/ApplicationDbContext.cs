using MarketPointApi.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketPointApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Esto sucede cuando hay entidades muchos a muchos y nos vemos obligados
        //a hacer uso de otra tabla que conecte de alguna manera ambas entidades
        //para que finalmente quedes conectadas 1:
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //Le estoy diciendo que para la entidad ProductosCategoria, la llave primaria
          //va estar compuesta de ProductoId y CategoriaId 
          //LLAVE COMPUESTA: Que ningun campo cumple por si solo para ser llave primaria
          modelBuilder.Entity<ProductosCategorias>()
              .HasKey(x => new { x.ProductoId, x.CategoriaId });

          modelBuilder.Entity<ProductosVendedores>()
               .HasKey(x => new { x.ProductoId, x.VendedorId });


           base.OnModelCreating(modelBuilder);
        }
       




        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductosCategorias> ProductosCategorias { get; set; } //Extencion de dos tablas con conexion muchos a muchos
        public DbSet<ProductosVendedores> ProductosVendedores { get; set; } //Extencion de dos tablas con conexion muchos a muchos
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<MiCompra> MisCompras { get; set; }
        public DbSet<MiVentaCompra> MisVentasCompras { get; set; }
        public DbSet<MedioDePago> MediosDePago { get; set; }
        //public DbSet<ComprobantePago> ComprobantesDePago { get; set; }
    }
}
