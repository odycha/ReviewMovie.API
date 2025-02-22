using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewMovie.API.Data;
using System;

namespace ReviewMovie.API.Data.Configurations
{
	public class ReviewConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.HasData(
				// Reviews for The Shawshank Redemption
				new Review
				{
					Id = 1,
					ReviewerName = "Alice",
					Rating = 7,
					Comment = "An unforgettable masterpiece.",
					MovieId = 1
				},
				new Review
				{
					Id = 2,
					ReviewerName = "Bob",
					Rating = 9,
					Comment = "A deeply moving story of hope.",
					MovieId = 1
				},

				// Reviews for The Godfather
				new Review
				{
					Id = 3,
					ReviewerName = "Charlie",
					Rating = 5,
					Comment = "The greatest crime film ever made.",
					MovieId = 2
				},
				new Review
				{
					Id = 4,
					ReviewerName = "David",
					Rating = 6,
					Comment = "A timeless classic with incredible performances.",
					MovieId = 2
				},

				// Reviews for The Dark Knight
				new Review
				{
					Id = 5,
					ReviewerName = "Eve",
					Rating = 10,
					Comment = "Heath Ledger's Joker is legendary.",
					MovieId = 3
				},
				new Review
				{
					Id = 6,
					ReviewerName = "Frank",
					Rating = 4,
					Comment = "A dark and thrilling superhero film.",
					MovieId = 3
				},

				// Reviews for Pulp Fiction
				new Review
				{
					Id = 7,
					ReviewerName = "Grace",
					Rating = 8,
					Comment = "Quentin Tarantino at his finest.",
					MovieId = 4
				},
				new Review
				{
					Id = 8,
					ReviewerName = "Hank",
					Rating = 5,
					Comment = "Brilliant dialogues and unforgettable characters.",
					MovieId = 4
				},

				// Reviews for Forrest Gump
				new Review
				{
					Id = 9,
					ReviewerName = "Ivy",
					Rating = 10,
					Comment = "A heartwarming tale of an extraordinary life.",
					MovieId = 5
				},
				new Review
				{
					Id = 10,
					ReviewerName = "Jack",
					Rating = 6,
					Comment = "Tom Hanks delivers a phenomenal performance.",
					MovieId = 5
				},

				// Reviews for Inception
				new Review
				{
					Id = 11,
					ReviewerName = "Karen",
					Rating = 9,
					Comment = "A mind-bending sci-fi thriller.",
					MovieId = 6
				},
				new Review
				{
					Id = 12,
					ReviewerName = "Leo",
					Rating = 7,
					Comment = "Nolan's masterpiece with stunning visuals.",
					MovieId = 6
				},

				// Reviews for Fight Club
				new Review
				{
					Id = 13,
					ReviewerName = "Mia",
					Rating = 10,
					Comment = "A gripping and thought-provoking film.",
					MovieId = 7
				},
				new Review
				{
					Id = 14,
					ReviewerName = "Nate",
					Rating = 9,
					Comment = "A mind-blowing twist and brilliant acting.",
					MovieId = 7
				},

				// Reviews for The Matrix
				new Review
				{
					Id = 15,
					ReviewerName = "Olivia",
					Rating = 5,
					Comment = "A sci-fi revolution in filmmaking.",
					MovieId = 8
				},
				new Review
				{
					Id = 16,
					ReviewerName = "Paul",
					Rating = 9,
					Comment = "An iconic action-packed adventure.",
					MovieId = 8
				},

				// Reviews for Interstellar
				new Review
				{
					Id = 17,
					ReviewerName = "Quinn",
					Rating = 9,
					Comment = "A visually stunning and emotional journey.",
					MovieId = 9
				},
				new Review
				{
					Id = 18,
					ReviewerName = "Rachel",
					Rating = 9,
					Comment = "One of the best space movies ever made.",
					MovieId = 9
				},

				// Reviews for The Lord of the Rings: The Return of the King
				new Review
				{
					Id = 19,
					ReviewerName = "Steve",
					Rating = 6,
					Comment = "An epic conclusion to an incredible trilogy.",
					MovieId = 10
				},
				new Review
				{
					Id = 20,
					ReviewerName = "Tina",
					Rating = 9,
					Comment = "Visually breathtaking and emotionally powerful.",
					MovieId = 10
				}
			);
		}
	}
}
