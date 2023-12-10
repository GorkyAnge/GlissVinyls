using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;
using System.Security.Claims;

namespace ProductoAppMVC.Controllers
{
    public class InicioController : Controller
    {
		private readonly IAPIServiceUsuario _apiServiceUsuario;


		public InicioController(IAPIServiceUsuario apiServiceUsuario)
		{
			_apiServiceUsuario = apiServiceUsuario;
		}

		public IActionResult Registrarse()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Registrarse(Usuario usuario)
		{
			// Verifica si el campo "Rol" está vacío
			if (string.IsNullOrWhiteSpace(usuario.Rol))
			{
				// Establece el valor por defecto "usuario"
				usuario.Rol = "usuario";
			}

			Usuario usuario_creado = await _apiServiceUsuario.SaveUsuario(usuario);

			if (usuario_creado.IdUsuario > 0)
                ViewBag.IdUsuario = usuario_creado.IdUsuario;
				ViewBag.Nombre = usuario_creado.Nombre;
            return RedirectToAction("Index", "Tienda", new { idUsuario = ViewBag.IdUsuario, nombre = ViewBag.Nombre });

            ViewData["Mensaje"] = "No se pudo crear el usuario";
			return View("Index");
		}

		public IActionResult IniciarSesion()
		{
			return View();
		}

        
		[HttpPost]
		public async Task<IActionResult> IniciarSesion(string Correo, string Contrasenia)
		{
			Usuario usuario_encontrado = await _apiServiceUsuario.GetUsuario(Correo, Contrasenia);

			if (usuario_encontrado == null)
			{
				TempData["Mensaje"] = "No se encontraron coincidencias";
				return RedirectToAction("Index", "Inicio");
			}

			// Almacena el IdUsuario en ViewBag
			ViewBag.IdUsuario = usuario_encontrado.IdUsuario;
			ViewBag.Nombre = usuario_encontrado.Nombre;

			if (usuario_encontrado.Rol == "admin")
			{
				return RedirectToAction("Index", "Admin");
			}

			return RedirectToAction("Index", "Tienda", new { idUsuario = ViewBag.IdUsuario, nombre = ViewBag.Nombre });
		}			



		public IActionResult Index(Usuario usuario_encontrado)
		{
			return View(usuario_encontrado);
		}
	}

}
