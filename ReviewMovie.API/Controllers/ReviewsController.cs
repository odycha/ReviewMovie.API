using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;
using ReviewMovie.API.Exceptions;
using ReviewMovie.API.Models.Review;

namespace ReviewMovie.API.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
    [Authorize]
    [ApiVersion("1.0")]
	public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsController(IMapper mapper, IReviewsRepository reviewsRepository)
        {
            _mapper = mapper;
			_reviewsRepository = reviewsRepository;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _reviewsRepository.GetAllAsync();
            var reviewsDto = _mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewsDto);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _reviewsRepository.GetAsync(id);
        
            var reviewDto = _mapper.Map<ReviewDto>(review);

            return Ok(reviewDto);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> PutReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
            {
				throw new BadRequestException("Wrong id given");
			}

            var review = await _reviewsRepository.GetAsync(id);
            if(review == null)
            {
				throw new NotFoundException(nameof(PutReview), id);
			}

            _mapper.Map(reviewDto, review);

            try
            {
                await _reviewsRepository.UpdateAsync(review);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateReviewDto>> PostReview(CreateReviewDto createReviewDto)
        {
            var review = _mapper.Map<Review>(createReviewDto);
            await _reviewsRepository.AddAsync(review);

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewsRepository.GetAsync(id);
            if (review == null)
            {
				throw new NotFoundException(nameof(DeleteReview), id);
			}

            await _reviewsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> ReviewExists(int id)
        {
            return await _reviewsRepository.ExistsAsync(id);
        }
    }
}
