﻿using System.ComponentModel.DataAnnotations;

namespace ReviewMovie.API.Core.Models.User
{
	public class LoginDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = " Your Password is limited to {2} to {1} characters",
			MinimumLength = 6)]
		public string Password { get; set; }
	}
}
