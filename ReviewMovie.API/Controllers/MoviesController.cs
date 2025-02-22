﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Data;
using ReviewMovie.API.Models.Movie;
using ReviewMovie.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using ReviewMovie.API.Exceptions;
using Asp.Versioning;

namespace ReviewMovie.API.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize]
	[ApiVersion("1.0")]
	public class MoviesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMoviesRepository _moviesRepository;

		public MoviesController(IMapper mapper, IMoviesRepository moviesRepository)
		{
			_mapper = mapper;
			_moviesRepository = moviesRepository;
		}

		// GET: api/Movies
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetMovieDto>>> GetMovies()
		{
			var movies = await _moviesRepository.GetAllAsync();

			var moviesDto = _mapper.Map<List<GetMovieDto>>(movies);

			return Ok(moviesDto);
		}

		// GET: api/Movies/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MovieDto>> GetMovie(int id)
		{
			var movie = await _moviesRepository.GetDetails(id);

			var moviesDto = _mapper.Map<MovieDto>(movie);

			return Ok(moviesDto);
		}

		// PUT: api/Movies/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> PutMovie(int id, UpdateMovieDto updateMovieDto)
		{
			if (id != updateMovieDto.Id)
			{
				throw new BadRequestException("Wrong id given");
			}

			var movie = await _moviesRepository.GetAsync(id); //now movie is being tracked

			if (movie == null)
			{
				throw new NotFoundException(nameof(PutMovie), id);
			}

			_mapper.Map(updateMovieDto, movie);

			try
			{
				await _moviesRepository.UpdateAsync(movie);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (! await CountryExists(id))
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
		[Authorize(Roles = "Administrator")]
		public async Task<ActionResult<Movie>> PostMovie(CreateMovieDto createMovie)
		{
			var movie = _mapper.Map<Movie>(createMovie);

			await _moviesRepository.AddAsync(movie);

			return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
		}

		// DELETE: api/Movies/5
		[HttpDelete("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteMovie(int id)
		{
			var country = await _moviesRepository.GetAsync(id);
			if(country == null)
			{
				throw new NotFoundException(nameof(DeleteMovie), id);
			}

			await _moviesRepository.DeleteAsync(id);

			return NoContent();
		}

		private async Task<bool> CountryExists(int id)
		{
			return await _moviesRepository.ExistsAsync(id);
		}
	}
}
