using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Models.DTOs;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZMartialArtsGymFinderAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public class GymController : ControllerBase
	{
		private readonly IGymRepository _gymRepo;
		private readonly IMapper _mapper;
		private readonly DbSet<Gym> _gymsDbSet;
		private readonly ApplicationDbContext _db;

		public GymController(ApplicationDbContext db, IGymRepository gymRepo, IMapper mapper)
		{
			_db = db ??	throw new Exception("ApplicationDbContext is null");
			_gymsDbSet = db.Gyms ?? throw new Exception("ApplicationDbContext has no Regions"); ;
			_gymRepo = gymRepo ?? throw new Exception("IRegionRepository is null");
			_mapper = mapper ?? throw new Exception("IMapper is null");
		}

		/// <summary>
		/// Get list of Gyms
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<GymDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetAllGyms()
		{
			ICollection<Gym> gymsList = _gymRepo?.GetAll(_gymsDbSet);

			if (gymsList == null || _mapper == null)
				return NotFound();

			List<GymDto> gymsDtos = new List<GymDto>();

			foreach(var martialArt in gymsList)
			{
				gymsDtos.Add(_mapper.Map<GymDto>(martialArt));
			}

			return Ok(gymsDtos);
		}

		/// <summary>
		/// Get individual Gym
		/// </summary>
		/// <param name="id"> The Id of the Gym </param>
		/// <returns></returns>
		[HttpGet("{id:int}", Name = nameof(GetGym))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GymDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult GetGym(int id)
		{
			var gym = _gymRepo?.Get(id, _gymsDbSet);

			if (gym == null || _mapper == null)
				return NotFound();

			GymDto gymDto = _mapper.Map<GymDto>(gym);
			return Ok(gymDto);
		}

		/// <summary>
		/// Create Gym
		/// </summary>
		/// <param name = "gymDto"> The Gym details </param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(GymDto))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateMartialArt([FromBody] GymCreateDto gymDto)
		{
			if (_mapper == null || gymDto == null || _gymRepo == null)
				return BadRequest(ModelState);

			if (_gymRepo.DoesEntryExist(gymDto.Name, _gymsDbSet))
			{
				ModelState.AddModelError("", $"{gymDto.Name} Region already exists");
				return StatusCode(404, ModelState);
			}

			Gym gym = _mapper.Map<Gym>(gymDto);

			if(_gymRepo.TryCreateEntry(gym, _db, _gymsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when saving the record {gym.Name}");
				return StatusCode(500, ModelState);
			}

			return CreatedAtRoute(nameof(GetGym), new { id = gym.Id }, gym);
		}

		/// <summary>
		/// Update Gym
		/// </summary>
		/// <param name="id"> The Id of the Gym </param>
		/// <param name = "gymDto"> The Gym details </param>
		/// <returns></returns>
		[HttpPatch("{id:int}", Name = nameof(UpdateGym))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult UpdateGym(int id, [FromBody] GymUpdateDto gymDto)
		{
			if (gymDto == null || _gymRepo == null || _mapper == null)
				return BadRequest(ModelState);

			if (id != gymDto.Id)
			{
				ModelState.AddModelError("", "id did not match id in the body");
				return StatusCode(404, ModelState);
			}

			Gym gym = _mapper.Map<Gym>(gymDto);

			if(_gymRepo.TryUpdateEntry(gym, _db, _gymsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when updating the record {gym.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		/// <summary>
		/// Delete Gym
		/// </summary>
		/// <param name="id"> The Id of the Gym </param>
		/// <returns></returns>
		[HttpDelete("{id:int}", Name = nameof(DeleteGym))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteGym(int id)
		{
			if (_gymRepo == null || _mapper == null)
				return BadRequest(ModelState);

			Gym gym = _gymRepo.Get(id, _gymsDbSet);

			if(gym == null)
			{
				ModelState.AddModelError("", $"The Region does not exist for deletion");
				return StatusCode(500, ModelState);
			}

			if(_gymRepo.TryDeleteEntry(gym, _db, _gymsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when deleting the record {gym.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
