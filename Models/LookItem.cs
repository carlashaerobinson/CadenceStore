using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadenceStore.Models
{
	public class LookItem
	{
		[Key]
		public int LookItemId { get; set; }

		[Required]
		public int LookId { get; set; }

		[ForeignKey("LookId")]
		public Look? Look { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public ClothingItem? ClothingItem { get; set; }
	}
}