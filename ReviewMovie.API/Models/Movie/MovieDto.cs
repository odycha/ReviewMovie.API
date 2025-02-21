using ReviewMovie.API.Models.Review;

namespace ReviewMovie.API.Models.Movie
{
	public class MovieDto
	{
        public int Id { get; set; }
		public string Title { get; set; }
		public virtual IList<ReviewDto>? Reviews { get; set; }
    }
}
