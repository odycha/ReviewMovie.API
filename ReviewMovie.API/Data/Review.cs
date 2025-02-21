using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewMovie.API.Data
{
	public class Review
	{
		public int Id { get; set; }
		public string ReviewerName { get; set; } = string.Empty;
		public int Rating { get; set; } // Rating out of 10
		public string Comment { get; set; } = string.Empty;

		[ForeignKey(nameof(MovieId))]
		public int MovieId { get; set; }
		public Movie Movie { get; set; }
	}
}
