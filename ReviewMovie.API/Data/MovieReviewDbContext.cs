using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Configurations;

namespace ReviewMovie.API.Data
{
	public class MovieReviewDbContext : IdentityDbContext<ApiUser>
	{

		public MovieReviewDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Review> Reviews { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new MovieConfiguration());
			modelBuilder.ApplyConfiguration(new ReviewConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
		}
	}

}