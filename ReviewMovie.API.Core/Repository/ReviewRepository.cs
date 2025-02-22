using AutoMapper;
using ReviewMovie.API.Core.Contracts;
using ReviewMovie.API.Data;
using ReviewMovies.API.Data;

namespace ReviewMovie.API.Core.Repository
{
	public class ReviewRepository : GenericRepository<Review>, IReviewsRepository
	{
		private readonly MovieReviewDbContext _context;
		private readonly IMapper _mapper;
		public ReviewRepository(MovieReviewDbContext context, IMapper mapper) : base(context, mapper)
		{
			_context = context;
			_mapper = mapper;
		}

	}
}
