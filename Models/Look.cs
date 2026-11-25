using System.ComponentModel.DataAnnotations;

namespace CadenceStore.Models
{
	public class Look
	{
		[Key]
		public int LookId { get; set; }

		[Required(ErrorMessage = "Look title is required.")]
		[StringLength(100)]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Style tag is required.")]
		[StringLength(50)]
		public string StyleTag { get; set; } = string.Empty; // e.g., Streetwear, Smart Casual, Weekend

		[Required(ErrorMessage = "Description is required.")]
		[StringLength(500)]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Cover image URL is required.")]
		[StringLength(255)]
		public string CoverImageUrl { get; set; } = "/images/looks/placeholder.jpg";

		public ICollection<LookItem> LookItems { get; set; } = new List<LookItem>();
	}
}