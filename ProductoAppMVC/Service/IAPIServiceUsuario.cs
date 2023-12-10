using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
	public interface IAPIServiceUsuario
	{

        public Task<List<Usuario>> GetUsuarios();
        public Task<Usuario> GetUsuario(string email, string password);
        public Task<Usuario> GetUsuarioPorId(int id);
		
		public Task<Usuario> SaveUsuario(Usuario usuario);

        public Task<Usuario> PutUsuario(int IdUsuario, Usuario usuario);
        public Task<Boolean> DeleteUsuario(int IdUsuario);
    }
}
