using ReviewMovie.API.Data;

namespace ReviewMovie.API.Core.Contracts
{
	public interface IMoviesRepository : IGenericRepository<Movie>
	{
		Task<Movie> GetDetails(int id);
	}
}
