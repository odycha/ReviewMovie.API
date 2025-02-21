using AutoMapper;
using ReviewMovie.API.Data;
using ReviewMovie.API.Models.Movie;
using ReviewMovie.API.Models.Review;

namespace ReviewMovie.API.Configurations
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Movie, BaseMovieDto>().ReverseMap();
			CreateMap<Movie, CreateMovieDto>().ReverseMap();
			CreateMap<Movie, GetMovieDto>().ReverseMap();
			CreateMap<Movie, MovieDto>().ReverseMap();
			CreateMap<Movie, UpdateMovieDto>().ReverseMap();

			CreateMap<Review, BaseReviewDto>().ReverseMap();
			CreateMap<Movie, CreateReviewDto>().ReverseMap();
			CreateMap<Movie, ReviewDto>().ReverseMap();

		}
	}
}
