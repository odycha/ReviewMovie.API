using System.ComponentModel.DataAnnotations;

namespace ReviewMovie.API.Models.Movie
{
	public abstract class BaseMovieDto
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		[Range(1800, 2100, ErrorMessage = "Invalid Year")]
		public int ReleaseDate { get; set; }
		[Required]
		public string Genre { get; set; }
	}
}
