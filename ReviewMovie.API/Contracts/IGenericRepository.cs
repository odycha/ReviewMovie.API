namespace ReviewMovie.API.Contracts
{
	public interface IGenericRepository<T> where T: class
	{
		public Task<T> GetAsync(int? id);
		public Task<List<T>> GetAllAsync();
		public Task UpdateAsync(T entity);
		public Task AddAsync(T entity);
		public Task DeleteAsync(int id);
		public Task<bool> ExistsAsync(int id);

	}
}
