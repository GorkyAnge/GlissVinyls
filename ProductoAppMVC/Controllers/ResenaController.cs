using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;
using ProductoAppMVC.Interfaces;

namespace ProductoAppMVC.Controllers
{
    public class ResenaController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAPIService _apiResena;

        private int _idUsuario;
        private string _nombre;
        private int _idProducto;


        // Constructor del controlador
        public ResenaController(IAPIService apiResena, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiResena = apiResena;
        }
        //SET ID USUARIO
        public void SetIdUsuario(int idUsuario)
        {
            _idUsuario = idUsuario;

            // Store the _idUsuario value in session
            _httpContextAccessor.HttpContext.Session.SetInt32("IdUsuario", _idUsuario);
        }
        //SET NOMBRE USUARIO
        public void SetNombreUsuario(string nombre)
        {
            // Ensure that nombre is not null or empty before storing in the session
            if (!string.IsNullOrEmpty(nombre))
            {
                _nombre = nombre;

                // Store the _nombre value in session
                _httpContextAccessor.HttpContext.Session.SetString("Nombre", _nombre);
            }
            // Optionally, you can log a warning or take other appropriate actions for a null or empty value.
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


        [HttpPost]
        public async Task<IActionResult> CreateResena(Resena resena)
        {
            Resena resena1 = await _apiResena.PostResena(resena);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Index(int IdProducto, int IdUsuario, string Nombre)
        {
            List<Resena> resenas = await _apiResena.GetResenas(IdProducto);
            ViewBag.IdProducto = IdProducto;
            ViewBag.IdUsuario = IdUsuario;
            ViewBag.Nombre = Nombre;
            SetIdProducto(IdProducto);
            SetIdUsuario(IdUsuario);
            SetNombreUsuario(Nombre);

            return View(resenas);
        }

        //Acción para agregar una reseña a un producto
        public IActionResult Create(int idProducto, int idUsuario, string nombre)
        {
            // Aquí puedes utilizar los valores recibidos para inicializar tu modelo de Resena
            // o realizar las operaciones necesarias.

            ViewBag.IdProducto = GetIdProducto();
            ViewBag.IdUsuario = GetIdUsuario();
            ViewBag.Nombre = GetNombreUsuario();

            // Resto del código...

            return View();
        }


        public async Task<IActionResult> Comment(int Id)
        {
            Resena resena = await _apiResena.GetResena(Id);

            if (resena != null)
            {
                return View(resena);
            }
            return RedirectToAction("DetailsReview");
        }
    }
}

