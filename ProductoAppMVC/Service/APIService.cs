using ProductoAppMVC.Interfaces;
using ProductoAppMVC.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
namespace ProductoAppMVC.Service
{
    public class APIService : IAPIService
    {
        public static string _baseUrl;
        public HttpClient _httpClient;
        public APIService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            _baseUrl = "https://localhost:5240/";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        //PRODUCTO

        public async Task<bool> DeleteProducto(int IdProducto)
        {
            var response = await _httpClient.DeleteAsync($"/api/Producto/{IdProducto}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<Producto> GetProducto(int Id)
        {
            //var response = await _httpClient.GetFromJsonAsync<Producto>($"api/Producto/{Id}");
            var response = await _httpClient.GetAsync($"/api/Producto/{Id}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Producto producto = JsonConvert.DeserializeObject<Producto>(json_response);
                return producto;
            }
            return new Producto();
        }

        public async Task<List<Producto>> GetProductos()
        {
            var response = await _httpClient.GetAsync("/api/Producto");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(json_response);
                return productos;
            }
            return new List<Producto>();

        }

        public async Task<Producto> PostProducto(Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Producto/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Producto producto2 = JsonConvert.DeserializeObject<Producto>(json_response);
                return producto2;
            }
            return new Producto();
        }

        public async Task<Producto> PutProducto(int IdProducto, Producto producto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Producto/{IdProducto}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Producto producto2 = JsonConvert.DeserializeObject<Producto>(json_response);
                return producto2;
            }
            return new Producto();
        }

        public async Task<List<Producto>> BuscarProductosPorNombre(string nombre)
        {
            var response = await _httpClient.GetAsync($"/api/Producto?nombre={nombre}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(json_response);
                return productos;
            }
            return new List<Producto>();
        }

        //PRODUCTOENCARRITO

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


        //RESENA

        public async Task<bool> DeleteResena(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Resena/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }
        public async Task<Resena> GetResena(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Resena/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Resena resena = JsonConvert.DeserializeObject<Resena>(json_response);
                return resena;
            }
            return new Resena();
        }
        public async Task<List<Resena>> GetListResenas()
        {
            var response = await _httpClient.GetAsync("/api/Resena");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<Resena> productos = JsonConvert.DeserializeObject<List<Resena>>(json_response);
                return productos;
            }
            return new List<Resena>();

        }
        public async Task<List<Resena>> GetResenas(int ProductoId)
        {
            var response = await _httpClient.GetAsync($"Resena/{ProductoId}");

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(json_response))
                {
                    List<Resena> resenas = JsonConvert.DeserializeObject<List<Resena>>(json_response);
                    return resenas;
                }
            }

            return new List<Resena>();
        }

        public async Task<Resena> PostResena(Resena resena)
        {
            var content = new StringContent(JsonConvert.SerializeObject(resena), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Resena/", content);

            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Resena resena2 = JsonConvert.DeserializeObject<Resena>(json_response);
                return resena2;
            }
            return new Resena();
        }

        //USUARIO

        public async Task<bool> DeleteUsuario(int IdUsuario)
        {
            var response = await _httpClient.DeleteAsync($"/api/Usuario/{IdUsuario}");
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }


        public async Task<List<Usuario>> GetUsuarios()
        {
            var response = await _httpClient.GetAsync("/api/Usuario");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json_response);
                return usuarios;
            }
            return new List<Usuario>();

        }
        public async Task<Usuario> GetUsuario(string Correo, string Contrasenia)
        {
            var response = await _httpClient.GetAsync($"/api/Usuario/{Correo}/{Contrasenia}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json_response);
                return usuario;
            }
            return null;
        }

        public async Task<Usuario> GetUsuarioPorId(int IdUsuario)
        {
            //var response = await _httpClient.GetFromJsonAsync<Producto>($"api/Producto/{Id}");
            var response = await _httpClient.GetAsync($"/api/Usuario/{IdUsuario}");
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json_response);
                return usuario;
            }
            return new Usuario();
        }

        public async Task<Usuario> PutUsuario(int IdUsuario, Usuario usuario)
        {
            var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Usuario/{IdUsuario}", content);
            if (response.IsSuccessStatusCode)
            {
                var json_response = await response.Content.ReadAsStringAsync();
                Usuario usuario2 = JsonConvert.DeserializeObject<Usuario>(json_response);
                return usuario2;
            }
            return new Usuario();
        }


        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Usuario/", content);

                if (response.IsSuccessStatusCode)
                {
                    var json_response = await response.Content.ReadAsStringAsync();
                    Usuario nuevo_usuario = JsonConvert.DeserializeObject<Usuario>(json_response);
                    return nuevo_usuario;
                }
                else
                {
                    // Log the unsuccessful response status code and any additional information.
                    Console.WriteLine($"Failed to save usuario. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis.
                Console.WriteLine($"An error occurred while saving usuario: {ex.Message}");
            }

            // Return null or throw an exception based on your application's requirements.
            return null;
        }




    }
}
