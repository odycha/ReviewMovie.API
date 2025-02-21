
using Microsoft.EntityFrameworkCore;
using ReviewMovie.API.Configurations;
using ReviewMovie.API.Data;
using Serilog;

namespace ReviewMovie.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("ReviewMovieDbConnectionString");

			builder.Services.AddDbContext<MovieReviewDbContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			// Add services to the container.

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



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseSerilogRequestLogging();

			app.UseHttpsRedirection();

			//Cors configuration(2/2)
			app.UseCors("AllowAll");

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
