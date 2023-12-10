using System.ComponentModel.DataAnnotations;

namespace ProductoAppMVC.Models
{
	public class ProductoEnCarrito
	{
		[Key]
		public int IdProductoEnCarrito { get; set; }

		[Required]
		public int IdUsuario { get; set; }

		[Required]
		public int IdProducto { get; set; }

		public string NombreProducto { get; set; }

		[Required]
		public int Cantidad { get; set; }



	}
}
