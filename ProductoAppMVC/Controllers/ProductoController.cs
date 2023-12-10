using Microsoft.AspNetCore.Mvc;
using ProductoAppMVC.Models;
using ProductoAppMVC.Service;
using System.Collections.ObjectModel;

namespace ProductoAppMVC.Controllers
{
    public class ProductoController : Controller
    {
        // Configuración para realizar solicitudes al API
        private readonly IAPIServiceProducto _apiService;

        // Constructor del controlador
        public ProductoController(IAPIServiceProducto apiService)
        {
            _apiService = apiService;
        }

        // Acción para mostrar una lista de productos
        public async Task<IActionResult> Index()
        {

            List<Producto> productos = await _apiService.GetProductos();
            return View(productos);
        }

        // GET: ProductoController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            Producto producto = await _apiService.GetProducto(Id);

            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            Producto producto1 = await _apiService.PostProducto(producto);
            return RedirectToAction("Index");
        }



        // GET: ProductoController/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            Producto producto = await _apiService.GetProducto(Id);
            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            // Retrieve the existing product from the API
            Producto existingProduct = await _apiService.GetProducto(producto.IdProducto);

            if (existingProduct != null)
            {
                // Update the existing product with the new data
                existingProduct.Nombre = producto.Nombre;
                existingProduct.Descripcion = producto.Descripcion;
                existingProduct.Stock = producto.Stock;
                existingProduct.Imagen = producto.Imagen;

                // Call the API to update the product
                Producto updatedProduct = await _apiService.PutProducto(producto.IdProducto, existingProduct);

                // Redirect to the Index action after a successful update
                return RedirectToAction("Index");
            }

            // Redirect to the Index action if the product was not found
            return RedirectToAction("Index");
        }


        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            Boolean producto2 = await _apiService.DeleteProducto(Id);
            if (producto2 != false)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
