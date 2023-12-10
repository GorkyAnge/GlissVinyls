using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
    public interface IAPIServiceProductoEnCarrito
    {
        public Task<List<ProductoEnCarrito>> ObtenerProductosEnCarritoAsync(int idUsuario);
        public Task AgregarProductoAlCarritoAsync(int idUsuario, int idProducto);
        public Task EliminarProductoDelCarritoAsync(int idUsuario, int idProducto);
    }
}
