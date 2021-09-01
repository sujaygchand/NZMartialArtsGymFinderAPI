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

		public DbSet<Region> Regions { get; set; }
		public DbSet<Gym> Gyms { get; set; }
		public DbSet<MartialArt> MartialArts { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
