using Microsoft.EntityFrameworkCore;
using NZMartialArtsGymFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<RegionDTO> Regions { get; set; }
	}
}
