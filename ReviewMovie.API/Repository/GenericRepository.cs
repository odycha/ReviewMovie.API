using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;
using ReviewMovie.API.Exceptions;
using ReviewMovie.API.Model;

namespace ReviewMovie.API.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T: class
	{
		private readonly MovieReviewDbContext _context;
		private readonly IMapper _mapper;

		public GenericRepository(MovieReviewDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}
		public async Task<T> GetAsync(int? id)
		{
			var result = await _context.Set<T>().FindAsync(id);
			if (result is null)
			{
				throw new NotFoundException(typeof(T).Name, id.HasValue ? id : "No Key Provided");
			}
			return result;
		}
		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
		{
			var totalSize = await _context.Set<T>().CountAsync();
			var items = await _context.Set<T>()
				.Skip(queryParameters.StartIndex)
				.Take(queryParameters.PageSize)
				.ProjectTo<TResult>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new PagedResult<TResult>
			{
				Items = items,
				PageNumber = queryParameters.PageNumber,
				RecordNumber = queryParameters.PageSize,
				TotalCount = totalSize
			};
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
			if (entity is null)
			{
				throw new NotFoundException(typeof(T).Name, id);
			}
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
