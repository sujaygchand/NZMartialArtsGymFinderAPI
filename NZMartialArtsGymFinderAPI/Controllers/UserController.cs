using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Controllers
{
	[Route("api/[controller]")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepo;

		public UserController(IUserRepository userRepo)
		{
			_userRepo = userRepo ?? throw new Exception("UserRepository is null");
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody] AuthenticationModel model)
		{
			var user = _userRepo.Authenticate(model.Username, model.Password);

			if (user == null)
				 return BadRequest(new { message = "Username or password is incorrect" });

			return Ok();
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public IActionResult Register([FromBody] AuthenticationModel model)
		{
			bool isUniqueUser = _userRepo.IsUniqueUser(model.Username);

			if (!isUniqueUser)
				return BadRequest(new { message = "Username already exists" });

			var user = _userRepo.Register(model);

			if (user == null)
				return BadRequest(new { message = "Error while registering" });

			return Ok();
		}
	}
}
