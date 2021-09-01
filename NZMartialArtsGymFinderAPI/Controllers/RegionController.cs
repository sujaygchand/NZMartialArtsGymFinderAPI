﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public RegionController(IRegionRepository regionRepo, IMapper mapper)
		{
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
			ICollection<Region> regionList = _regionRepo?.GetAllRegions();

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
			var region = _regionRepo?.GetRegion(id);

			if (region == null || _mapper == null)
				return NotFound();

			RegionDto regionDto = _mapper.Map<RegionDto>(region);
			return Ok(regionDto);
		}

		[HttpPost]
		[ProducesResponseType(201, Type = typeof(RegionDto))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult CreateRegion([FromBody] RegionDto regionDto)
		{
			if (_mapper == null || regionDto == null || _regionRepo == null)
				return BadRequest(ModelState);

			if (_regionRepo.DoesRegionExist(regionDto.Name))
			{
				ModelState.AddModelError("", $"{regionDto.Name} Region already exists");
				return StatusCode(404, ModelState);
			}

			Region region = _mapper.Map<Region>(regionDto);

			if(_regionRepo.TryCreateRegion(region) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when saving the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return CreatedAtRoute(nameof(GetRegion), new { id = region.Id }, region);
		}

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

			if(_regionRepo.TryUpdateRegion(region) == false)
			{
				ModelState.AddModelError("", $"Something went wrong when updating the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		[HttpDelete("{id:int}", Name = nameof(DeleteRegion))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public IActionResult DeleteRegion(int id)
		{
			if (_regionRepo == null || _mapper == null)
				return BadRequest(ModelState);

			Region region = _regionRepo.GetRegion(id);

			if(region == null)
			{
				ModelState.AddModelError("", $"Something went wrong when deleting the record {region.Name}");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}
	}
}