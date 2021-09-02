using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class Region : BaseModel
	{
		public string DiallingCode { get; set; }
		public byte[] Picture { get; set; }
	}
}
