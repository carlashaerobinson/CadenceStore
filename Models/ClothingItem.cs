using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadenceStore.Models
{
	public class ClothingItem
	{
		[Key]
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Item name is required.")]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Description is required.")]
		[StringLength(500)]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Category is required.")]
		[StringLength(50)]
		public string Category { get; set; } = string.Empty; // Tops, Bottoms, Outerwear, Footwear, Accessories

		[Required(ErrorMessage = "Size is required.")]
		[StringLength(20)]
		public string Size { get; set; } = string.Empty; // XS, S, M, L, XL, One Size

		[Required(ErrorMessage = "Color is required.")]
		[StringLength(30)]
		public string Color { get; set; } = string.Empty;

		[Required(ErrorMessage = "Price is required.")]
		[Range(0.01, 10000.00, ErrorMessage = "Price must be between $0.01 and $10,000.00.")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Stock quantity is required.")]
		[Range(0, 1000, ErrorMessage = "Stock must be between 0 and 1,000.")]
		public int StockQuantity { get; set; }

		[Required(ErrorMessage = "Image URL is required.")]
		[StringLength(255)]
		public string ImageUrl { get; set; } = "/images/products/placeholder.jpg";

		public ICollection<LookItem> LookItems { get; set; } = new List<LookItem>();
	}
}