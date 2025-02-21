using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Repository
{
	public class MovieRepository : GenericRepository<Movie>, IMoviesRepository
	{
		private readonly MovieReviewDbContext _context;

		public MovieRepository(MovieReviewDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<Movie> GetDetails(int id)
		{
			return await _context.Movies.Include(q => q.Reviews)
				.FirstOrDefaultAsync(q => q.Id == id);
		}
	}
}
