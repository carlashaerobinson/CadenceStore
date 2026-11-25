using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CadenceStore.Models;

namespace CadenceStore.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ClothingItem> ClothingItems { get; set; }
		public DbSet<Look> Looks { get; set; }
		public DbSet<LookItem> LookItems { get; set; }
	}
}