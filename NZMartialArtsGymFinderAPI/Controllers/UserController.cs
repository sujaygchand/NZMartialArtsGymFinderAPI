using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Models.DTOs;
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
		private readonly IMapper _mapper;

		public UserController(IUserRepository userRepo, IMapper mapper)
		{
			_userRepo = userRepo ?? throw new Exception("UserRepository is null");
			_mapper = mapper ?? throw new Exception("mapper is null");
		}

		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody] AuthenticationModel model)
		{
			var user = _userRepo.Authenticate(model.Username, model.Password);

			if (user == null)
				 return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(user);
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

		/// <summary>
		/// Get list of Users.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<UserDto>))]
		//[Authorize(Roles = "admin")]
		public IActionResult GetAllUsers()
		{
			ICollection<User> userList = _userRepo.GetAllUsers();

			List<UserDto> userDtos = new List<UserDto>();

			foreach (var user in userList)
			{
				userDtos.Add(_mapper.Map<UserDto>(user));
			}

			return Ok(userDtos);
		}


		/// <summary>
		/// Get individual User
		/// </summary>
		/// <param name="id"> The Id of the User </param>
		/// <returns></returns>
		[HttpGet("{id:int}", Name = nameof(GetUser))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		[Authorize(Roles = "admin")]
		public IActionResult GetUser(int id)
		{
			User user = _userRepo?.GetUser(id);

			if (user == null)
				return NotFound();

			UserDto userDto = _mapper.Map<UserDto>(user);
			return Ok(userDto);
		}

		[HttpDelete("{id:int}", Name = nameof(DeleteUser))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "admin")]
		public IActionResult DeleteUser(int id)
		{
			User user = _userRepo.GetUser(id);

			if(user == null)
			{
				ModelState.AddModelError("", "User dose not exists for deletion");
				return StatusCode(404, ModelState);
			}

			if (_userRepo.TryDeleteUser(user) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when deleting the record {user.Username}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
