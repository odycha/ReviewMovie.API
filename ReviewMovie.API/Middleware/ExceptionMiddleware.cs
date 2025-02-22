﻿using ReviewMovie.API.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace ReviewMovie.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Something Went wrong while processing {context.Request.Path}");
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/json";
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			var errorDetails = new ErrorDetails
			{
				ErrorType = "Failure",
				ErrorMessage = ex.Message,
			};

			switch (ex)
			{
				case NotFoundException notFoundException:
					statusCode = HttpStatusCode.NotFound;
					errorDetails.ErrorType = "Not Found";
					break;
				case BadRequestException badRequestException:
					statusCode = HttpStatusCode.BadRequest;
					errorDetails.ErrorType = "Bad Request";
					break;
				default:
					break;
			}

			string response = JsonConvert.SerializeObject(errorDetails);
			context.Response.StatusCode = (int)statusCode;
			return context.Response.WriteAsync(response);
		}
	}

	public class ErrorDetails
	{
		public string ErrorType { get; set; }
		public string ErrorMessage { get; set; }
	}
}





