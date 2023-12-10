using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;

namespace ProductoAppMVC.Controllers
{
    public class TiendaController : Controller
    {
        private readonly IAPIServiceProducto _apiService;
        private readonly IAPIServiceProductoEnCarrito _apiServiceProductoEnCarrito;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAPIServiceCarrito _apiServiceCarrito;

        private int _idUsuario;
        private string _nombre;
        private int _idProducto;



        // Constructor del controlador
        public TiendaController(IAPIServiceProducto apiService, IAPIServiceProductoEnCarrito apiServiceProductoEnCarrito, IHttpContextAccessor httpContextAccessor)
        {

            _apiService = apiService;
            _apiServiceProductoEnCarrito = apiServiceProductoEnCarrito;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Carrito()
        {
            ViewBag.IdUsuario = _idUsuario;

            return RedirectToAction("Index", "Carrito", new { idUsuario = ViewBag.IdUsuario}); // Esto renderizará la vista "Index.cshtml" del Carrito
        }
        //SET ID USUARIO
        public void SetIdUsuario(int idUsuario)
        {
            _idUsuario = idUsuario;

            // Store the _idUsuario value in session
            _httpContextAccessor.HttpContext.Session.SetInt32("IdUsuario", _idUsuario);
        }
        //SET NOMBRE USUARIO
        private void SetNombreUsuario(string nombre)
        {
            if (nombre != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("Nombre", nombre);
            }
        }

        //SET ID PRODUCTO
        public void SetIdProducto(int idProducto)
        {
            _idProducto = idProducto;

            // Store the _idUsuario value in session
            _httpContextAccessor.HttpContext.Session.SetInt32("IdProducto", _idProducto);
        }
        //GET ID PRODUCTO
        public int GetIdProducto()
        {
            // Retrieve the _idUsuario value from session
            return _httpContextAccessor.HttpContext.Session.GetInt32("IdProducto") ?? 0;
        }
        //GET NOMBRE USUARIO
        public string GetNombreUsuario()
        {
            // Retrieve the _idUsuario value from session
            return _httpContextAccessor.HttpContext.Session.GetString("Nombre") ?? "";
        }

        //GET ID USUARIO
        public int GetIdUsuario()
        {
            // Retrieve the _idUsuario value from session
            return _httpContextAccessor.HttpContext.Session.GetInt32("IdUsuario") ?? 0;
        }


        // Acción para mostrar una lista de productos
        public async Task<IActionResult> Index(string buscar, int idUsuario, string nombre)
        {
            List<Producto> productos;
            ViewBag.IdUsuario = idUsuario;
            ViewBag.Nombre = nombre;
            
            SetIdUsuario(idUsuario);
            SetNombreUsuario(nombre);
            


            if (!string.IsNullOrEmpty(buscar))
            {
                // Realiza una búsqueda de productos por nombre y filtra la lista
                productos = await _apiService.BuscarProductosPorNombre(buscar);
            }
            else
            {
                // Si no se proporciona un término de búsqueda, obtén todos los productos
                productos = await _apiService.GetProductos();
            }

            return View(productos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Producto producto = await _apiService.GetProducto(id);
            SetIdProducto(id);

            ViewBag.IdUsuario=GetIdUsuario();

            ViewBag.Nombre = GetNombreUsuario();

            ViewBag.IdProducto = GetIdProducto();


            if (producto != null)
            {
                return View(producto);
            }

            return RedirectToAction("Index", new { IdUsuario = ViewBag.IdUsuario, IdProducto = ViewBag.IdProducto, Nombre = ViewBag.Nombre});
        }


    }
}
