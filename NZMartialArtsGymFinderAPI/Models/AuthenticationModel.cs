using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class AuthenticationModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public string Role { get; set; }
	}
}
