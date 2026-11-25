using Microsoft.EntityFrameworkCore;
using CadenceStore.Models;

namespace CadenceStore.Data
{
	public static class DbInitializer
	{
		public static async Task InitializeAsync(IServiceProvider serviceProvider)
		{
			using var context = new ApplicationDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

			if (await context.ClothingItems.AnyAsync())
			{
				return; // Database already seeded
			}

			var items = new ClothingItem[]
			{
				new() { Name = "Heavyweight Boxy Tee", Description = "100% organic cotton relaxed streetwear t-shirt.", Category = "Tops", Size = "L", Color = "Washed Charcoal", Price = 38.00m, StockQuantity = 25, ImageUrl = "/images/products/boxy-tee.jpg" },
				new() { Name = "Utility Cargo Parachute Pants", Description = "Relaxed-fit nylon cargo trousers with adjustable cuffs.", Category = "Bottoms", Size = "M", Color = "Olive Drab", Price = 74.00m, StockQuantity = 18, ImageUrl = "/images/products/cargo-pants.jpg" },
				new() { Name = "Retro Court Low Sneakers", Description = "Minimalist leather low-top sneakers with gum sole.", Category = "Footwear", Size = "10", Color = "Off-White", Price = 110.00m, StockQuantity = 15, ImageUrl = "/images/products/court-sneakers.jpg" },
				new() { Name = "Linen Resort Camp Shirt", Description = "Breathable button-down collar shirt for warm days.", Category = "Tops", Size = "M", Color = "Sandstone", Price = 55.00m, StockQuantity = 20, ImageUrl = "/images/products/linen-shirt.jpg" },
				new() { Name = "Pleated Wide-Leg Trousers", Description = "Tailored drape trousers with front pleats.", Category = "Bottoms", Size = "M", Color = "Cream", Price = 82.00m, StockQuantity = 14, ImageUrl = "/images/products/pleated-trousers.jpg" },
				new() { Name = "Woven Leather Crossbody Bag", Description = "Compact everyday crossbody with brushed brass hardware.", Category = "Accessories", Size = "One Size", Color = "Espresso", Price = 68.00m, StockQuantity = 12, ImageUrl = "/images/products/crossbody-bag.jpg" },
				new() { Name = "Structured Wool Overcoat", Description = "Double-breasted mid-length coat for layered looks.", Category = "Outerwear", Size = "L", Color = "Camel", Price = 165.00m, StockQuantity = 10, ImageUrl = "/images/products/wool-overcoat.jpg" },
				new() { Name = "Fine-Knit Merino Turtleneck", Description = "Lightweight breathable merino wool layering top.", Category = "Tops", Size = "M", Color = "Obsidian Black", Price = 64.00m, StockQuantity = 22, ImageUrl = "/images/products/merino-turtleneck.jpg" },
				new() { Name = "Suede Penny Loafers", Description = "Hand-stitched suede loafers with cushioned insole.", Category = "Footwear", Size = "9", Color = "Tobacco", Price = 125.00m, StockQuantity = 11, ImageUrl = "/images/products/suede-loafers.jpg" }
			};

			context.ClothingItems.AddRange(items);
			await context.SaveChangesAsync();

			var looks = new Look[]
			{
				new() { Title = "Streetwear Essential", StyleTag = "Streetwear", Description = "Effortless everyday proportions pairing heavyweight jersey with technical cargos.", CoverImageUrl = "/images/looks/streetwear-essential.jpg" },
				new() { Title = "Weekend Brunch", StyleTag = "Relaxed Resort", Description = "Airy linen textures and warm neutral tones built for sunny afternoons.", CoverImageUrl = "/images/looks/weekend-brunch.jpg" },
				new() { Title = "Smart Casual", StyleTag = "Smart Casual", Description = "Sharp tailored outerwear layered over fine merino knitwear.", CoverImageUrl = "/images/looks/smart-casual.jpg" }
			};

			context.Looks.AddRange(looks);
			await context.SaveChangesAsync();

			var lookItems = new LookItem[]
			{
                // Streetwear Essential -> Tee, Cargos, Sneakers
                new() { LookId = looks[0].LookId, ProductId = items[0].ProductId },
				new() { LookId = looks[0].LookId, ProductId = items[1].ProductId },
				new() { LookId = looks[0].LookId, ProductId = items[2].ProductId },
                // Weekend Brunch -> Linen Shirt, Pleated Trousers, Crossbody Bag
                new() { LookId = looks[1].LookId, ProductId = items[3].ProductId },
				new() { LookId = looks[1].LookId, ProductId = items[4].ProductId },
				new() { LookId = looks[1].LookId, ProductId = items[5].ProductId },
                // Smart Casual -> Overcoat, Turtleneck, Loafers
                new() { LookId = looks[2].LookId, ProductId = items[6].ProductId },
				new() { LookId = looks[2].LookId, ProductId = items[7].ProductId },
				new() { LookId = looks[2].LookId, ProductId = items[8].ProductId }
			};

			context.LookItems.AddRange(lookItems);
			await context.SaveChangesAsync();
		}
	}
}