using System.ComponentModel.DataAnnotations;

namespace ReviewMovie.API.Models.User
{
	public class ApiUserDto : LoginDto
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		
	}
}
