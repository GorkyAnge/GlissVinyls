using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
    public interface IAPIServiceProductoEnCarrito
    {
        Task<ProductoEnCarrito> ObtenerProductoEnCarrito(int IdProductoEnCarrito);
        Task<List<ProductoEnCarrito>> ObtenerProductosEnCarrito(int IdUsuario);
        Task<ProductoEnCarrito> AgregarAlCarrito(int IdUsuario, int IdProducto);
        Task<Boolean> EliminarProductoEnCarrito(int IdProductoEnCarrito);
        Task<Boolean> EliminarProductosEnCarritoPorUsuario(int IdUsuario); // Agregado nuevo método
    }

}
