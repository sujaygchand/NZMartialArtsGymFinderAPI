﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class Gym
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public enum MartialArts {
			[Description("MMA")]
			MMA,
			[Description("Muay Thai | Kickboxing")]
			MuayThai,
			[Description("BJJ")]
			BJJ,
			[Description("Wrestling")]
			Wrestling,
			[Description("Boxing")]
			Boxing
		}

		[Required]
		public ICollection<MartialArts> MartialArtsTaught { get; set; }

		public string Website { get; set; }

		public byte[] Picture { get; set; }

		[Required]
		public string Address { get; set; }
		[Required]
		public string ZipCode { get; set; }
		public string ContactEmail { get; set; }
		[Required]
		public int RegionId { get; set; }
		[ForeignKey("RegionId")]
		public Region Region { get; set; }

	}
}