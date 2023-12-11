using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Service;
using ProductoAppMVC.Models;


public class CarritoController : Controller
{
    private readonly IAPIServiceProductoEnCarrito _productoEnCarritoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _idUsuario;


    public CarritoController(IAPIServiceProductoEnCarrito productoEnCarritoService, IHttpContextAccessor httpContextAccessor)
    {
        _productoEnCarritoService = productoEnCarritoService;
        _httpContextAccessor = httpContextAccessor;
    }

    //GET ID USUARIO
    public int GetIdUsuario()
    {
        // Retrieve the _idUsuario value from session
        return _httpContextAccessor.HttpContext.Session.GetInt32("IdUsuario") ?? 0;
    }

    //SET ID USUARIO
    public void SetIdUsuario(int idUsuario)
    {
        _idUsuario = idUsuario;

        // Store the _idUsuario value in session
        _httpContextAccessor.HttpContext.Session.SetInt32("IdUsuario", _idUsuario);
    }

    // Acción para ver los productos en el carrito
    public async Task<IActionResult> Index(int IdUsuario)
    {
        SetIdUsuario(IdUsuario);
        ViewBag.IdUsuario = IdUsuario;
        List<ProductoEnCarrito> productosEnCarrito = await _productoEnCarritoService.ObtenerProductosEnCarrito(IdUsuario);
        return View(productosEnCarrito);
    }

    // Acción para agregar un producto al carrito
    public async Task<IActionResult> AgregarAlCarrito(int IdUsuario, int IdProducto)
    {
        SetIdUsuario(IdUsuario);
        await _productoEnCarritoService.AgregarAlCarrito(IdUsuario, IdProducto);
        return RedirectToAction("Index", new { IdUsuario });
    }

    public async Task<IActionResult> EliminarDelCarrito(int IdProductoEnCarrito)
    {
        Boolean producto2 = await _productoEnCarritoService.EliminarProductoEnCarrito(IdProductoEnCarrito);
        if (producto2 != false)
        {
            return RedirectToAction("Index", new { IdUsuario=GetIdUsuario() });
        }
        return RedirectToAction("Index", new { IdUsuario = GetIdUsuario() });
    }

    public async Task<IActionResult> ComprarCarrito(int IdUsuario)
    {
        Boolean producto2 = await _productoEnCarritoService.EliminarProductosEnCarritoPorUsuario(IdUsuario);
        if (producto2 != false)
        {
            return RedirectToAction("Index", new { IdUsuario = GetIdUsuario() });
        }
        return RedirectToAction("Index", new { IdUsuario = GetIdUsuario() });
    }

}
