using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewMovie.API.Data;
using System;

namespace ReviewMovie.API.Data.Configurations
{
	public class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.HasData(
				new Movie
				{
					Id = 1,
					Title = "The Shawshank Redemption",
					Description = "A story of hope and friendship.",
					ReleaseDate = 1994,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 2,
					Title = "The Godfather",
					Description = "An iconic mafia story.",
					ReleaseDate = 1972,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 3,
					Title = "The Dark Knight",
					Description = "Batman faces the Joker.",
					ReleaseDate = 2008,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 4,
					Title = "Pulp Fiction",
					Description = "Interwoven crime stories.",
					ReleaseDate = 1994,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 5,
					Title = "Forrest Gump",
					Description = "Life seen through Forrest's eyes.",
					ReleaseDate = 1994,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 6,
					Title = "Inception",
					Description = "A journey into dreams.",
					ReleaseDate = 2010,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 7,
					Title = "Fight Club",
					Description = "A psychological thriller.",
					ReleaseDate = 1999,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 8,
					Title = "The Matrix",
					Description = "Reality is not what it seems.",
					ReleaseDate = 1999,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 9,
					Title = "Interstellar",
					Description = "A journey beyond the stars.",
					ReleaseDate = 2014,
					Genre = "Drama"
				},
				new Movie
				{
					Id = 10,
					Title = "The Lord of the Rings: The Return of the King",
					Description = "The final battle for Middle-earth.",
					ReleaseDate = 2003,
					Genre = "Drama"
				}
			);
		}
	}
}
