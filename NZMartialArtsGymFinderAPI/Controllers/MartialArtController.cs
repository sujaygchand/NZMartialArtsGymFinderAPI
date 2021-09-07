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
	public class MartialArtController : ControllerBase
	{
		private readonly IMartialArtsRepository _martialArtsRepo;
		private readonly IMapper _mapper;
		private readonly DbSet<MartialArt> _martialArtsDbSet;
		private readonly ApplicationDbContext _db;

		public MartialArtController(ApplicationDbContext db, IMartialArtsRepository martialArtsRepo, IMapper mapper)
		{
			_db = db ??	throw new Exception("ApplicationDbContext is null");
			_martialArtsDbSet = db.MartialArts ?? throw new Exception("ApplicationDbContext has no Regions"); ;
			_martialArtsRepo = martialArtsRepo ?? throw new Exception("IRegionRepository is null");
			_mapper = mapper ?? throw new Exception("IMapper is null");
		}

		/// <summary>
		/// Get list of Martial Arts
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<MartialArtDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetAllMartialArts()
		{
			ICollection<MartialArt> martialArtsList = _martialArtsRepo?.GetAll(_martialArtsDbSet);

			if (martialArtsList == null || _mapper == null)
				return NotFound();

			List<MartialArtDto> martialArtsDtos = new List<MartialArtDto>();

			foreach(var martialArt in martialArtsList)
			{
				martialArtsDtos.Add(_mapper.Map<MartialArtDto>(martialArt));
			}

			return Ok(martialArtsDtos);
		}

		/// <summary>
		/// Get individual Martial Art
		/// </summary>
		/// <param name="id"> The Id of the Martial Art </param>
		/// <returns></returns>
		[HttpGet("{id:int}", Name = nameof(GetMartialArt))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MartialArtDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult GetMartialArt(int id)
		{
			var martialArt = _martialArtsRepo?.Get(id, _martialArtsDbSet);

			if (martialArt == null || _mapper == null)
				return NotFound();

			MartialArtDto martialArtDto = _mapper.Map<MartialArtDto>(martialArt);
			return Ok(martialArtDto);
		}

		/// <summary>
		/// Create Martial Art
		/// </summary>
		/// <param name = "martialArtDto"> The Martial Art details </param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(MartialArtDto))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "admin, Admin")]
		public IActionResult CreateMartialArt([FromBody] MartialArtDto martialArtDto)
		{
			if (_mapper == null || martialArtDto == null || _martialArtsRepo == null)
				return BadRequest(ModelState);

			if (_martialArtsRepo.DoesEntryExist(martialArtDto.Name, _martialArtsDbSet))
			{
				ModelState.AddModelError("", $"{martialArtDto.Name} Region already exists");
				return StatusCode(404, ModelState);
			}
			
			martialArtDto.Id = 0;

			MartialArt martialArt = _mapper.Map<MartialArt>(martialArtDto);

			if(_martialArtsRepo.TryCreateEntry(martialArt, _db, _martialArtsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when saving the record {martialArt.Name}");
				return StatusCode(500, ModelState);
			}

			return CreatedAtRoute(nameof(GetMartialArt), new { id = martialArt.Id }, martialArt);
		}

		/// <summary>
		/// Update Martial Art
		/// </summary>
		/// <param name="id"> The Id of the Martial Art </param>
		/// <param name = "martialArtDto"> The Martial Art details </param>
		/// <returns></returns>
		[HttpPatch("{id:int}", Name = nameof(UpdateMartialArt))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "admin, Admin")]
		public IActionResult UpdateMartialArt(int id, [FromBody] MartialArtDto martialArtDto)
		{
			if (martialArtDto == null || _martialArtsRepo == null || _mapper == null)
				return BadRequest(ModelState);

			if (id != martialArtDto.Id)
			{
				ModelState.AddModelError("", "id did not match id in the body");
				return StatusCode(404, ModelState);
			}

			MartialArt martialArt = _mapper.Map<MartialArt>(martialArtDto);

			if(_martialArtsRepo.TryUpdateEntry(martialArt, _db, _martialArtsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when updating the record {martialArt.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		/// <summary>
		/// Delete Region
		/// </summary>
		/// <param name="id"> The Id of the Region </param>
		/// <returns></returns>
		[HttpDelete("{id:int}", Name = nameof(DeleteMartialArt))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[Authorize(Roles = "admin, Admin")]
		public IActionResult DeleteMartialArt(int id)
		{
			if (_martialArtsRepo == null || _mapper == null)
				return BadRequest(ModelState);

			MartialArt martialArt = _martialArtsRepo.Get(id, _martialArtsDbSet);

			if(martialArt == null)
			{
				ModelState.AddModelError("", $"The Region does not exist for deletion");
				return StatusCode(500, ModelState);
			}

			if(_martialArtsRepo.TryDeleteEntry(martialArt, _db, _martialArtsDbSet) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when deleting the record {martialArt.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
