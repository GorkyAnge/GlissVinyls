using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
    public interface IAPIServiceCarrito
    {
        public Task<List<ProductoEnCarrito>> ObtenerCarritoAsync(int idUsuario);
        public Task AgregarProductoAlCarritoAsync(int idUsuario, int idProducto);
        public Task EliminarProductoDelCarritoAsync(int idUsuario, int idProducto);

    }
}
