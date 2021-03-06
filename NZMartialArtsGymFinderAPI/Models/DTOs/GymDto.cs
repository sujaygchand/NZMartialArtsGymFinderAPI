using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models.DTOs
{
	public class GymDto : BaseModel
	{
		[Required]
		public string MartialArtIds { get; set; }

		public string Website { get; set; }

		public byte[] Picture { get; set; }

		[Required]
		public string Address { get; set; }
		[Required]
		public string ZipCode { get; set; }
		public string ContactEmail { get; set; }
		[Required]
		public int RegionId { get; set; }

		public RegionDto Region { get; set; }
	}
}
