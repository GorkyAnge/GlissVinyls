using Newtonsoft.Json;
using ProductoAppMVC.Models;
using System.Net;
using System.Text;

namespace ProductoAppMVC.Service
{
	public class APIServiceUsuario : IAPIServiceUsuario
	{
		public static string _baseUrl;
		public HttpClient _httpClient;
		public APIServiceUsuario()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();

			_baseUrl = builder.GetSection("ApiSettings: BaseUrl").Value;
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new Uri("https://apiproductos20231211072423.azurewebsites.net/");
		}

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
