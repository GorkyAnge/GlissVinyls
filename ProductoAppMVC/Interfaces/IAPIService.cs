using ProductoAppMVC.Models;

namespace ProductoAppMVC.Interfaces
{
    public interface IAPIService
    {
        //PRODUCTO
        public Task<List<Producto>> GetProductos();
        public Task<Producto> GetProducto(int Id);
        public Task<Producto> PostProducto(Producto producto);
        public Task<Producto> PutProducto(int IdProducto, Producto producto);
        public Task<Boolean> DeleteProducto(int IdProducto);
        public Task<List<Producto>> BuscarProductosPorNombre(string nombre);

        //PRODUCTOENCARRITO

        public Task<ProductoEnCarrito> ObtenerProductoEnCarrito(int IdProductoEnCarrito);
        public Task<List<ProductoEnCarrito>> ObtenerProductosEnCarrito(int IdUsuario);
        public Task<ProductoEnCarrito> AgregarAlCarrito(int IdUsuario, int IdProducto);
        public Task<Boolean> EliminarProductoEnCarrito(int IdProductoEnCarrito);
        public Task<Boolean> EliminarProductosEnCarritoPorUsuario(int IdUsuario); // Agregado nuevo método

        //RESENA

        public Task<Resena> GetResena(int Id);
        public Task<Resena> PostResena(Resena resena);
        public Task<List<Resena>> GetResenas(int id);
        public Task<List<Resena>> GetListResenas();
        public Task<Boolean> DeleteResena(int IdResena);


        //USUARIO

        public Task<List<Usuario>> GetUsuarios();
        public Task<Usuario> GetUsuario(string email, string password);
        public Task<Usuario> GetUsuarioPorId(int id);
        public Task<Usuario> SaveUsuario(Usuario usuario);
        public Task<Usuario> PutUsuario(int IdUsuario, Usuario usuario);
        public Task<Boolean> DeleteUsuario(int IdUsuario);

    }
}
