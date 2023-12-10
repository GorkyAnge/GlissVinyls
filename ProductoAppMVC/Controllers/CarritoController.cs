using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Service;
using ProductoAppMVC.Models;

public class CarritoController : Controller
{
    private readonly IAPIServiceProductoEnCarrito _apiServiceProductoEnCarrito;

    public CarritoController(IAPIServiceProductoEnCarrito apiServiceProductoEnCarrito)
    {
        _apiServiceProductoEnCarrito = apiServiceProductoEnCarrito;
    }

    public async Task<IActionResult> Index(int idUsuario)
    {
        // Obtener productos en el carrito para el usuario dado desde el servicio API
        var productosEnCarrito = await _apiServiceProductoEnCarrito.ObtenerProductosEnCarritoAsync(idUsuario);

        // Puedes pasar la lista de productos al modelo de la vista
        return View(productosEnCarrito);
    }

    public async Task<IActionResult> AgregarAlCarrito(int idUsuario, int idProducto)
    {
        // Lógica para agregar un producto al carrito a través del servicio API
        await _apiServiceProductoEnCarrito.AgregarProductoAlCarritoAsync(idUsuario, idProducto);

        // Redirigir a la acción Index para mostrar el carrito actualizado
        return RedirectToAction("Index", new { idUsuario });
    }

    public async Task<IActionResult> EliminarDelCarrito(int idUsuario, int idProducto)
    {
        // Lógica para eliminar un producto del carrito a través del servicio API
        await _apiServiceProductoEnCarrito.EliminarProductoDelCarritoAsync(idUsuario, idProducto);

        // Redirigir a la acción Index para mostrar el carrito actualizado
        return RedirectToAction("Index", new { idUsuario });
    }
}
