using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Repository
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
