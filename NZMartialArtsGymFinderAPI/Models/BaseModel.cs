using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Models
{
	public class BaseModel
	{
		[Key]
		[Column(Order = 0)]
		public int Id { get; set; }

		[Required]
		[Column(Order = 1)]
		public string Name { get; set; }
	}
}
