
using Asp.Versioning;
using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReviewMovie.API.Configurations;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;
using ReviewMovie.API.Middleware;
using ReviewMovie.API.Repository;
using Serilog;
using System.Text;

namespace ReviewMovie.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("ReviewMovieDbConnectionString");



			// Add services to the container.

			//Set up EF and point the database
			builder.Services.AddDbContext<MovieReviewDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			//Add Identity Core to the API
			builder.Services.AddIdentityCore<ApiUser>()
				.AddRoles<IdentityRole>()
				.AddTokenProvider<DataProtectorTokenProvider<ApiUser>>("ReviewMovieApi")
				.AddEntityFrameworkStores<MovieReviewDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddAutoMapper(typeof(MapperConfig));

			//Cors configuration(1/2)
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll",
					b => b.AllowAnyHeader()
					.AllowAnyOrigin()
					.AllowAnyMethod());
			});

			//versioning configuration (1/2)
			var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				// Use whatever reader you want
				options.ApiVersionReader = ApiVersionReader.Combine(
					new QueryStringApiVersionReader("api-version"),
					new HeaderApiVersionReader("X-Version"),
					new MediaTypeApiVersionReader("ver")
				);
			});// Nuget Package: Asp.Versioning.Mvc

			//versioning configuration (2/2)
			apiVersioningBuilder.AddApiExplorer(options =>
			{
				// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
				// note: the specified format code will format the version as "'v'major[.minor][-status]"
				options.GroupNameFormat = "'v'VVV";

				// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
				// can also be used to control the format of the API version in route templates
				options.SubstituteApiVersionInUrl = true;
			}); // Nuget Package: Asp.Versioning.Mvc.ApiExplorer

			//Serilog and Seq Configuration
			builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<IMoviesRepository, MovieRepository>();
			builder.Services.AddScoped<IReviewsRepository, ReviewRepository>();
			builder.Services.AddScoped<IAuthManager, AuthManager>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero,
					ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
					ValidAudience = builder.Configuration["JwtSettings:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
				};
			});

			//Caching configuration
			builder.Services.AddResponseCaching(options =>
			{
				//in bytes what is the largest cachable data
				options.MaximumBodySize = 1024;
				//different cache for api/Hotels vs api/hotels
				options.UseCaseSensitivePaths = true;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				
			}

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseSerilogRequestLogging();

			app.UseHttpsRedirection();

			//Cors configuration(2/2)
			app.UseCors("AllowAll");

			// Response caching configuration
			app.UseResponseCaching();
			app.Use(async (context, next) =>
			{
				context.Response.GetTypedHeaders().CacheControl =
					new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
					{
						Public = true,
						MaxAge = TimeSpan.FromSeconds(10)
					};
				context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
					new string[] { "Accept-Encoding" };

				await next();
			});

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
