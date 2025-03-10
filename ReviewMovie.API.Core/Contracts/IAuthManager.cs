﻿using Microsoft.AspNetCore.Identity;
using ReviewMovie.API.Core.Models.User;

namespace ReviewMovie.API.Core.Contracts
{
	public interface IAuthManager
	{
		//Returns a list of errors that occured - not simply true or false whether completed
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<string> CreateRefreshToken();
		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
	}
}
