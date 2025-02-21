using AutoMapper;
using ReviewMovie.API.Data;
using ReviewMovie.API.Models.Movie;
using ReviewMovie.API.Models.Review;
using ReviewMovie.API.Models.User;

namespace ReviewMovie.API.Configurations
{
	public class MapperConfig : Profile
	{
		public MapperConfig()
		{
			CreateMap<Movie, CreateMovieDto>().ReverseMap();
			CreateMap<Movie, GetMovieDto>().ReverseMap();
			CreateMap<Movie, MovieDto>().ReverseMap();
			CreateMap<Movie, UpdateMovieDto>().ReverseMap();

			CreateMap<Review, CreateReviewDto>().ReverseMap();
			CreateMap<Review, ReviewDto>().ReverseMap();

			CreateMap<ApiUserDto, ApiUser>().ReverseMap();

		}
	}
}
