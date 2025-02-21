
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReviewMovie.API.Configurations;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Data;
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

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				
			}

			app.UseSwagger();
			app.UseSwaggerUI();

			app.UseSerilogRequestLogging();

			app.UseHttpsRedirection();

			//Cors configuration(2/2)
			app.UseCors("AllowAll");

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
