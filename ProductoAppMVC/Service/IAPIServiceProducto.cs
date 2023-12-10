using ProductoAppMVC.Models;

namespace ProductoAppMVC.Service
{
    public interface IAPIServiceProducto
    {
        public Task<List<Producto>> GetProductos();

        public Task<Producto> GetProducto(int Id);

        public Task<Producto> PostProducto(Producto producto);

        public Task<Producto> PutProducto(int IdProducto, Producto producto);
        public Task<Boolean> DeleteProducto(int IdProducto);
        public Task<List<Producto>> BuscarProductosPorNombre(string nombre);
    }
}
