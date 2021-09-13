using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Username { get; set; }
		public string Email { get; set; }
		[Required]
		public byte[] Password { get; set; }

		public byte[] PasswordKey { get; set; }

		public string Role { get; set; }
		[NotMapped]
		public string Token { get; set; }
	}
}
