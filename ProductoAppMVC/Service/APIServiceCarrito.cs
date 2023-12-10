using Newtonsoft.Json;
using ProductoAppMVC.Models;
using System.Text;

namespace ProductoAppMVC.Service
{
    public class APIServiceCarrito : IAPIServiceCarrito
    {
        public static string _baseUrl;
        public HttpClient _httpClient;

        public APIServiceCarrito()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSettings: BaseUrl").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://apiproductos20231127081048.azurewebsites.net/");
        }

        public async Task<List<ProductoEnCarrito>> ObtenerCarritoAsync(int idUsuario)
        {
            var endpoint = $"/api/ProductoEnCarrito/{idUsuario}";

            var response = await _httpClient.GetAsync(_baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductoEnCarrito>>(content);
            }

            // Manejar el error de acuerdo a tus necesidades (lanzar excepción, retornar un valor predeterminado, etc.)
            return new List<ProductoEnCarrito>();
        }

        public async Task AgregarProductoAlCarritoAsync(int idUsuario, int idProducto)
        {
            var endpoint = $"/api/ProductoEnCarrito/{idUsuario}/AgregarProducto";

            // Crear el objeto para enviar en el cuerpo de la solicitud
            var contenido = new StringContent(JsonConvert.SerializeObject(new { IdUsuario = idUsuario, IdProducto = idProducto }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + endpoint, contenido);

            response.EnsureSuccessStatusCode();
        }


        public async Task EliminarProductoDelCarritoAsync(int idUsuario, int idProducto)
        {
            var endpoint = $"/api/ProductoEnCarrito/{idUsuario}/EliminarProducto/{idProducto}";

            var response = await _httpClient.DeleteAsync(_baseUrl + endpoint);

            response.EnsureSuccessStatusCode();
        }

    }
}
