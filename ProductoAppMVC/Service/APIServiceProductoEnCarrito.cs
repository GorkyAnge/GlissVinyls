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
            _httpClient.BaseAddress = new Uri("https://apiproductos20231211072423.azurewebsites.net/");
        }

        public async Task<ProductoEnCarrito> ObtenerProductoEnCarrito(int IdProductoEnCarrito)
        {
            var response = await _httpClient.GetAsync($"api/ProductoEnCarrito/IdProductoEnCarrito/{IdProductoEnCarrito}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductoEnCarrito>(content);
            }

            throw new Exception($"Error al obtener el producto en el carrito. StatusCode: {response.StatusCode}");
        }

        public async Task<List<ProductoEnCarrito>> ObtenerProductosEnCarrito(int IdUsuario)
        {
            var response = await _httpClient.GetAsync($"api/ProductoEnCarrito/ObtenerProductosEnCarrito/IdUsuario/{IdUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductoEnCarrito>>(content);
            }

            throw new Exception($"Error al obtener los productos en el carrito. StatusCode: {response.StatusCode}");
        }

        public async Task<ProductoEnCarrito> AgregarAlCarrito(int IdUsuario, int IdProducto)
        {
            var response = await _httpClient.PostAsync($"api/ProductoEnCarrito/AgregarAlCarrito/{IdUsuario}/{IdProducto}", null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductoEnCarrito>(content);
            }

            throw new Exception($"Error al agregar el producto al carrito. StatusCode: {response.StatusCode}");


        }


        public async Task<bool> EliminarProductoEnCarrito(int IdProductoEnCarrito)
        {
            var response = await _httpClient.DeleteAsync($"api/ProductoEnCarrito/EliminarProductoEnCarrito/{IdProductoEnCarrito}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }

        public async Task<Boolean> EliminarProductosEnCarritoPorUsuario(int IdUsuario)
        {
            var response = await _httpClient.DeleteAsync($"api/ProductoEnCarrito/EliminarProductosEnCarritoPorUsuario/{IdUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }



    }
}
