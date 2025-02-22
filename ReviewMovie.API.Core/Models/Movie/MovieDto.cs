using ReviewMovie.API.Core.Models.Review;

namespace ReviewMovie.API.Core.Models.Movie
{
	public class MovieDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public virtual IList<ReviewDto>? Reviews { get; set; }
	}
}
