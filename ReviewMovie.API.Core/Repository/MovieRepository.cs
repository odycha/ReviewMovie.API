using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Core.Contracts;
using ReviewMovie.API.Data;
using ReviewMovies.API.Data;

namespace ReviewMovie.API.Core.Repository
{
	public class MovieRepository : GenericRepository<Movie>, IMoviesRepository
	{
		private readonly MovieReviewDbContext _context;
		private readonly IMapper _mapper;

		public MovieRepository(MovieReviewDbContext context, IMapper mapper) : base(context, mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<Movie> GetDetails(int id)
		{
			return await _context.Movies.Include(q => q.Reviews)
				.FirstOrDefaultAsync(q => q.Id == id);
		}
	}
}
