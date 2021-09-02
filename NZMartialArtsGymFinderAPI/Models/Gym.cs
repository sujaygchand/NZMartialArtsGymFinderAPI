using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class Gym : BaseModel
	{
		[Required]
		public virtual ICollection<IdCollection> MartialArtIds { get; set; }

		public string Website { get; set; }

		public byte[] Picture { get; set; }

		[Required]
		public string Address { get; set; }
		[Required]
		public string ZipCode { get; set; }
		public string ContactEmail { get; set; }

		public string MobileNumber { get; set; }
		public string LandlineNumber { get; set; }
		[Required]
		public int RegionId { get; set; }
		[ForeignKey("RegionId")]
		public Region Region { get; set; }

	}
}
