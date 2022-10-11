using Azure.Storage.Blobs;

namespace MarketPointApi.Utilidades
{
    public class AlmacenadorAzureStorage : IAlmacenadorArchivos
    {
        //
        private string connectionString;

        public AlmacenadorAzureStorage(IConfiguration configuration)
        {
            //Inicializamos el campo connectionString a partir de configuration
            //Desde Iconfiguration podemos obtener informacion de nuestros proveedores de configuracion
            //Como por ejm en Program.cs .... En nuestro Program.cs tendremos un connectionString llamada AzureStorage
            //De esta manera nos vamos a poder comunicar con la instancia 'marketpoind_1664...' en azureStorage que acabamos de crear
            connectionString = configuration.GetConnectionString("AzureStorage");
        }


        //Un contenedor en azureStorage es como una carpeta
        public async Task<string> GuardarArchivo(string contenedor, IFormFile archivo)
        {

            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync(); //En caso de que no exista el contenedor me lo crea (primer caso: true)
            cliente.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            //Buscamos la extencion que el usuario haya subido en el frontEnd (img)
            var extension = Path.GetExtension(archivo.FileName);
            var archivoNombre = $"{Guid.NewGuid()}{extension}"; //De esta manera no tenemos coliciones de nombres a azureStorage
            var blob = cliente.GetBlobClient(archivoNombre);    //Le pasamos el nombre del archivo a azureStorage
            await blob.UploadAsync(archivo.OpenReadStream());
            return blob.Uri.ToString();                         //Retornamos la URL del archivo que hemos subido
        }

        //Metodo para borrar archivos
        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }

            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(archivo);
            // Finalente, borralo si existe
            await blob.DeleteIfExistsAsync();
        }

        //Metodo edicion : Actualizar imagen, borrar imagen anterior y quedarnos con la nueva
        public async Task<string> EditarArchivo(string contenedor, IFormFile archivo, string ruta)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenedor, archivo);
        }
    }

}

