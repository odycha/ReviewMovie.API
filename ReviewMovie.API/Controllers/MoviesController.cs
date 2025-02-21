using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Data;
using ReviewMovie.API.Models.Movie;

namespace ReviewMovie.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly MovieReviewDbContext _context;
		private readonly IMapper _mapper;

		public MoviesController(MovieReviewDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		// GET: api/Movies
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetMovieDto>>> GetMovies()
		{
			var movies = await _context.Movies.ToListAsync();

			var moviesDto = _mapper.Map<List<GetMovieDto>>(movies);

			return Ok(moviesDto);
		}

		// GET: api/Movies/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MovieDto>> GetMovie(int id)
		{
			var movie = await _context.Movies.Include(q => q.Reviews)
				.FirstOrDefaultAsync(q => q.Id == id);

			if (movie == null)
			{
				return NotFound();
			}

			var moviesDto = _mapper.Map<MovieDto>(movie);

			return Ok(moviesDto);
		}

		// PUT: api/Movies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutMovie(int id, UpdateMovieDto updateMovieDto)
		{
			if (id != updateMovieDto.Id)
			{
				return BadRequest();
			}

			var movie = _context.Movies.FindAsync(id); //now movie is being tracked

			if (movie == null)
			{
				return NotFound();
			}

			_mapper.Map(updateMovieDto, movie); 

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MovieExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Movies
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Movie>> PostMovie(CreateMovieDto createMovie)
		{
			var movie = _mapper.Map<Movie>(createMovie);

			_context.Movies.Add(movie);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
		}

		// DELETE: api/Movies/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			var movie = await _context.Movies.FindAsync(id);
			if (movie == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(movie);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool MovieExists(int id)
		{
			return _context.Movies.Any(e => e.Id == id);
		}
	}
}
