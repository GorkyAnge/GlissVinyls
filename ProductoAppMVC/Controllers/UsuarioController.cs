using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;
using ProductoAppMVC.Interfaces;

namespace ProductoAppMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IAPIService _apiService;


        public UsuarioController(IAPIService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> usuarios = await _apiService.GetUsuarios();
            return View(usuarios);
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            Usuario usuario1 = await _apiService.SaveUsuario(usuario);
            return RedirectToAction("Index");
        }



        // GET: UsuarioController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.IdUsuario = Id;
            Usuario usuario = await _apiService.GetUsuarioPorId(Id);
            if (usuario != null)
            {
                return View(usuario);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            // Retrieve the existing product from the API
            Usuario existingUser = await _apiService.GetUsuarioPorId(usuario.IdUsuario);

            if (existingUser != null)
            {
                // Update the existing product with the new data
                existingUser.Nombre = usuario.Nombre;
                existingUser.Apellidos = usuario.Apellidos;
                existingUser.Correo = usuario.Correo;
                existingUser.Contrasenia = usuario.Contrasenia;
                existingUser.Rol = usuario.Rol;

                // Call the API to update the product El TONY ES EL MAS GUAPO DEL MUNDO :) SEBAS SAPO
                Usuario updatedUser = await _apiService.PutUsuario(usuario.IdUsuario, existingUser);

                // Redirect to the Index action after a successful update
                return RedirectToAction("Index");
            }

            // Redirect to the Index action if the product was not found
            return RedirectToAction("Index");
        }


        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            Boolean producto2 = await _apiService.DeleteUsuario(Id);
            if (producto2 != false)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
