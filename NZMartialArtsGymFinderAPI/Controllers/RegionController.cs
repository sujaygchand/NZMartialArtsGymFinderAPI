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
	public class RegionController : ControllerBase
	{
		private readonly IRegionRepository _regionRepo;
		private readonly IMapper _mapper;
		private readonly DbSet<Region> _regions;
		private readonly ApplicationDbContext _db;

		public RegionController(ApplicationDbContext db, IRegionRepository regionRepo, IMapper mapper)
		{
			_db = db ??	throw new Exception("ApplicationDbContext is null");
			_regions = db.Regions ?? throw new Exception("ApplicationDbContext has no Regions"); ;
			_regionRepo = regionRepo ?? throw new Exception("IRegionRepository is null");
			_mapper = mapper ?? throw new Exception("IMapper is null");
		}

		/// <summary>
		/// Get list of Regions in NZ
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<RegionDto>))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetAllRegions()
		{
			ICollection<Region> regionList = _regionRepo?.GetAll(_regions);

			if (regionList == null || _mapper == null)
				return NotFound();

			List<RegionDto> regionDtos = new List<RegionDto>();

			foreach(var region in regionList)
			{
				regionDtos.Add(_mapper.Map<RegionDto>(region));
			}

			return Ok(regionDtos);
		}

		/// <summary>
		/// Get individual Region
		/// </summary>
		/// <param name="id"> The Id of the Region </param>
		/// <returns></returns>
		[HttpGet("{id:int}", Name = nameof(GetRegion))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesDefaultResponseType]
		public IActionResult GetRegion(int id)
		{
			var region = _regionRepo?.Get(id, _regions);

			if (region == null || _mapper == null)
				return NotFound();

			RegionDto regionDto = _mapper.Map<RegionDto>(region);
			return Ok(regionDto);
		}

		/// <summary>
		/// Create Region
		/// </summary>
		/// <param region = "regionDto"> The Region details </param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(201, Type = typeof(RegionDto))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateRegion([FromBody] RegionDto regionDto)
		{
			if (_mapper == null || regionDto == null || _regionRepo == null)
				return BadRequest(ModelState);

			if (_regionRepo.DoesEntryExist(regionDto.Name, _regions))
			{
				ModelState.AddModelError("", $"{regionDto.Name} Region already exists");
				return StatusCode(404, ModelState);
			}
			
			regionDto.Id = 0;

			Region region = _mapper.Map<Region>(regionDto);

			if(_regionRepo.TryCreateEntry(region, _db, _regions) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when saving the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return CreatedAtRoute(nameof(GetRegion), new { id = region.Id }, region);
		}

		/// <summary>
		/// Update Region
		/// </summary>
		/// <param name="id"> The Id of the Region </param>
		/// <param region = "regionDto"> The Region details </param>
		/// <returns></returns>
		[HttpPatch("{id:int}", Name = nameof(UpdateRegion))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult UpdateRegion(int id, [FromBody] RegionDto regionDto)
		{
			if (regionDto == null || _regionRepo == null || _mapper == null)
				return BadRequest(ModelState);

			if (id != regionDto.Id)
			{
				ModelState.AddModelError("", "id did not match id in the body");
				return StatusCode(404, ModelState);
			}

			Region region = _mapper.Map<Region>(regionDto);

			if(_regionRepo.TryUpdateEntry(region, _db, _regions) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when updating the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		/// <summary>
		/// Delete Region
		/// </summary>
		/// <param name="id"> The Id of the Region </param>
		/// <returns></returns>
		[HttpDelete("{id:int}", Name = nameof(DeleteRegion))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteRegion(int id)
		{
			if (_regionRepo == null || _mapper == null)
				return BadRequest(ModelState);

			Region region = _regionRepo.Get(id, _regions);

			if(region == null)
			{
				ModelState.AddModelError("", $"The Region does not exist for deletion");
				return StatusCode(500, ModelState);
			}

			if(_regionRepo.TryDeleteEntry(region, _db, _regions) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when deleting the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}
