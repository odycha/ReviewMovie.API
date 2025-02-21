using ReviewMovie.API.Data;

namespace ReviewMovie.API.Contracts
{
	public interface IMoviesRepository : IGenericRepository<Movie>
	{
		Task<Movie> GetDetails(int id);
	}
}
