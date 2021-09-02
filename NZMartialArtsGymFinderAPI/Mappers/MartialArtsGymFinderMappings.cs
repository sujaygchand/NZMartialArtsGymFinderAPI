using AutoMapper;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Mappers
{
	public class MartialArtsGymFinderMappings : Profile
	{
		public MartialArtsGymFinderMappings()
		{
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<MartialArt, MartialArtDto>().ReverseMap();
			CreateMap<Gym, GymDto>().ReverseMap();
			CreateMap<Gym, GymCreateDto>().ReverseMap();
			CreateMap<Gym, GymUpdateDto>().ReverseMap();
		}
	}
}
