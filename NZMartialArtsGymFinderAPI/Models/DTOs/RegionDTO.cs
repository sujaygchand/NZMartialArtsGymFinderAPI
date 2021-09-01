using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models.DTOs
{
	public class RegionDTO
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string AreaCode { get; set; }
	}
}
