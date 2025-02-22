using AutoMapper;
using ReviewMovie.API.Core.Models.Movie;
using ReviewMovie.API.Core.Models.Review;
using ReviewMovie.API.Core.Models.User;
using ReviewMovie.API.Data;

namespace ReviewMovie.API.Core.Configurations
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
