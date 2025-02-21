using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T: class
	{
		private readonly MovieReviewDbContext _context;

		public GenericRepository(MovieReviewDbContext context)
        {
			_context = context;
		}
		public async Task<T> GetAsync(int? id)
		{
			if (id == null)
			{
				return null;
			}
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}
		public async Task UpdateAsync(T entity)
		{
			_context.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task AddAsync(T entity)
		{
			_context.Add(entity); //equivalent to _context.Set<T>().Add(entity); EF auto detects entity type
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var entity = await GetAsync(id);
			_context.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync(int id)
		{
			var entity = await GetAsync(id);
			return entity != null;
		}
	}
}
