using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;

namespace ProductoAppMVC.Controllers
{
    public class ResenasController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAPIServiceResena _apiResena;
        private readonly IAPIServiceProducto _apiProducto;

        public ResenasController(IAPIServiceResena apiResena, IHttpContextAccessor httpContextAccessor, IAPIServiceProducto apiProducto)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiResena = apiResena;
            _apiProducto = apiProducto;
        }
        public async Task<IActionResult> Index()
        {
            //List<Resena> resenas = await _apiResena.GetListResenas();
            //return View(resenas);
            List<Producto> productos = await _apiProducto.GetProductos();
            return View(productos);
        }

        public async Task<IActionResult> ResenasPorProducto(int IdProducto)
        {

            //List<Resena> resenas = await _apiResena.GetListResenas();
            //return View(resenas);
            List<Resena> resenas = await _apiResena.GetResenas(IdProducto);
            return View(resenas);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Boolean resena2 = await _apiResena.DeleteResena(id);
            if (resena2 != false)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
