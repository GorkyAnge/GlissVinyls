using System.ComponentModel.DataAnnotations;

namespace ProductoAppMVC.Models
{
	public class Carrito
	{
		[Key]
		public int IdCarrito { get; set; }

		[Required]
		public int IdUsuario { get; set; }

		// Relación uno a muchos con los productos en el carrito
		public List<ProductoEnCarrito> ProductosEnCarrito { get; set; }

		// Precio total del carrito
		public decimal PrecioTotal { get; set; }
	}
}
