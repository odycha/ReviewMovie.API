using System.ComponentModel.DataAnnotations;

namespace ReviewMovie.API.Core.Models.Review
{
	public abstract class BaseReviewDto
	{
		[Required]
		public string ReviewerName { get; set; }
		[Required]
		[Range(1, 10, ErrorMessage = "The rating must be between 1 and 10")]
		public int Rating { get; set; }
		[Required]
		public string Comment { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int MovieId { get; set; }
	}
}
