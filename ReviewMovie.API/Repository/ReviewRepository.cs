using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Repository
{
	public class ReviewRepository : GenericRepository<Review>, IReviewsRepository
	{
		private readonly MovieReviewDbContext _context;
        public ReviewRepository(MovieReviewDbContext context) : base(context)
        {
			_context = context;
        }

	}
}
