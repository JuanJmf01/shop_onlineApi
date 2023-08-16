using AutoMapper;
using MarketPointApi.DTOs;
using MarketPointApi.Entidades;
using Microsoft.AspNetCore.Identity;

namespace MarketPointApi.Utilidades
{
    //Utilizamos Automaper para recorrer todas aquella informacion de las entidades que necesitemos
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Categoria, CategoriaDTO>().ReverseMap();


            CreateMap<Vendedor, VendedorDTO>().ReverseMap();
            CreateMap<VendedorCreacionDTO, Vendedor>();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<UsuarioCreacionDTO, Usuario>();


            CreateMap<ProductoCreacionDTO, Producto>()
            .ForMember(x => x.ImagenProducto, opciones => opciones.Ignore())
            .ForMember(x => x.ProductosCategorias, opciones => opciones.MapFrom(MapearProductosCategorias))
            .ForMember(x => x.ProductosVendedores, opciones => opciones.MapFrom(MapearProductosVendedores));

            CreateMap<Producto, ProductoDTO>()
                .ForMember(x => x.Categorias, options => options.MapFrom(MapearProductosCategorias))
                .ForMember(x => x.Vendedores, opciones => opciones.MapFrom(MapearProductosVendedores));
            ;

            CreateMap<IdentityUser, UsuarioAdminVendedor>();

            CreateMap<MiCompra, MisComprasDTO>().ReverseMap();
            CreateMap<MisComprasCreacionDTO, MiCompra>().ReverseMap()
                .ForMember(x => x.ImagenComprobante, opciones => opciones.Ignore());

            CreateMap<MiVentaCompra, MisVentasDTO>().ReverseMap();

            CreateMap<MedioDePago, MedioDePagoDTO>();
            CreateMap<MedioDePagoCreacionDTO, MedioDePago>()
                .ForMember(x => x.imagenMedioPago, opciones => opciones.Ignore());

            CreateMap<ComprobantePago, ComprobantePagoDTO>();
            CreateMap<ComprobantePagoCreacionDTO, ComprobantePago>()
                .ForMember(x => x.ImagenComprobante, opciones => opciones.Ignore());

        }

        private List<ProductosCategorias> MapearProductosCategorias(ProductoCreacionDTO productoCreacionDTO,
            Producto producto)
        {
            var resultado = new List<ProductosCategorias>();

            if (productoCreacionDTO.CategoriasIds == null) { return resultado; }

            foreach (var id in productoCreacionDTO.CategoriasIds)
            {
                resultado.Add(new ProductosCategorias() { CategoriaId = id });
            }

            return resultado;

        }


       private List<ProductosVendedores> MapearProductosVendedores(ProductoCreacionDTO productoCreacionDTO,
            Producto producto)
        {
            var resultado = new List<ProductosVendedores>();

            if (productoCreacionDTO.VendedoresIds == null) { return resultado; }

            foreach (var id in productoCreacionDTO.VendedoresIds)
            {
                resultado.Add(new ProductosVendedores() { VendedorId = id });
            }

            return resultado;

        }
        



        //
        private List<CategoriaDTO> MapearProductosCategorias(Producto producto, ProductoDTO productoDTO)
        {
            var resultado = new List<CategoriaDTO>();

            if(producto.ProductosCategorias != null)
            {
                foreach(var categoria in producto.ProductosCategorias)
                {
                    resultado.Add(new CategoriaDTO()
                    {
                        Id = categoria.CategoriaId,
                        Nombre = categoria.Categoria.Nombre
                    });
                }
            }

            return resultado;
        }

        private List<ProductoVendedorDTO> MapearProductosVendedores(Producto producto, ProductoDTO productoDTO)
        {
            var resultado = new List<ProductoVendedorDTO>();

            if (producto.ProductosCategorias != null)
            {
                foreach (var vendedor in producto.ProductosVendedores)
                {
                    resultado.Add(new ProductoVendedorDTO()
                    {
                        Id = vendedor.VendedorId,
                        Nombres = vendedor.Vendedor.Nombres,
                        Email = vendedor.Vendedor.Email,
                        NumeroCelular = vendedor.Vendedor.NumeroCelular,
                        Facebook = vendedor.Vendedor.Facebook,
                        Instagram = vendedor.Vendedor.Instagram

                    });
                }
            }

            return resultado;
        }










    }
}
