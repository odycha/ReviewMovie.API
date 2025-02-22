﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ReviewMovie.API.Contracts;
using ReviewMovie.API.Models.User;
using ReviewMovie.API.Repository;

namespace ReviewMovie.API.Controllers
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
	public class AccountController : ControllerBase
	{
		private readonly IAuthManager _authManager;
		private readonly ILogger<AccountController> _logger;

		public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
		{
			_authManager = authManager;
			_logger = logger;
		}


		// POST: api/Account/register
		[HttpPost]
		[Route("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Register([FromBody]ApiUserDto apiUserDto)
		{
			var errors = await _authManager.Register(apiUserDto);

			if (errors.Any())
			{
				foreach(var error in errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}
			return Ok();
		}


		//POST: api/Account/login
		[HttpPost]
		[Route("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult> Login(LoginDto loginDto)
		{
			var authResponse = await _authManager.Login(loginDto);

			if (authResponse == null)
			{
				return Unauthorized();
			}

			return Ok(authResponse);
		}

		// POST: api/Account/refreshtoken
		[HttpPost]
		[Route("refreshtoken")]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDto request)
		{
			var authResponse = await _authManager.VerifyRefreshToken(request);

			if (authResponse == null)
			{
				return Unauthorized();
			}

			return Ok(authResponse);
		}





	}
}
