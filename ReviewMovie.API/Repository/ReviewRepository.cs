using AutoMapper;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Repository
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
