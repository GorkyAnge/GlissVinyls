using Newtonsoft.Json;
using ProductoAppMVC.Models;
using System.Net;
using System.Text;

namespace ProductoAppMVC.Service
{
    public class APIServiceProductoEnCarrito : IAPIServiceProductoEnCarrito
    {
        public static string _baseUrl;
        public HttpClient _httpClient;

        public APIServiceProductoEnCarrito()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _baseUrl = builder.GetSection("ApiSettings: BaseUrl").Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://apiproductos20231127081048.azurewebsites.net/");
        }

        public async Task<List<ProductoEnCarrito>> ObtenerProductosEnCarritoAsync(int idUsuario)
        {
            var endpoint = $"/api/ProductoEnCarrito/{idUsuario}";

            var response = await _httpClient.GetAsync(_baseUrl + endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductoEnCarrito>>(content);
            }

            // Manejar el error de acuerdo a tus necesidades
            return new List<ProductoEnCarrito>();
        }

        public async Task AgregarProductoAlCarritoAsync(int idUsuario, int idProducto)
        {
            var endpoint = $"/api/ProductoEnCarrito/{idUsuario}/AgregarProducto";

            // Assuming you have some data to send in the request body
            var dataToSend = new
            {
                // ... populate your data properties here
            };

            // Serialize the data to JSON
            var jsonPayload = JsonConvert.SerializeObject(dataToSend);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);
           

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                // Log or inspect the response content for more details
                Console.WriteLine($"Response Content: {responseContent}");
            }


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
