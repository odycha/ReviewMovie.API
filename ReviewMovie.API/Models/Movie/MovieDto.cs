using ReviewMovie.API.Models.Review;

namespace ReviewMovie.API.Models.Movie
{
	public class MovieDto
	{
        public int Id { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
