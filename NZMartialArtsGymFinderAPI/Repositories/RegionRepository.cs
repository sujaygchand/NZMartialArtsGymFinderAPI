using NZMartialArtsGymFinderAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using NZMartialArtsGymFinderAPI.Repositories.Generic;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Utilities;

namespace NZMartialArtsGymFinderAPI.Repositories
{
	public class RegionRepository : GenericModelRepository<Region>, IRegionRepository
	{
		/*private readonly ApplicationDbContext _db;

		public RegionRepository(ApplicationDbContext db)
		{
			_db = db ?? throw new Exception("ApplicationDbContext does not exist");
		}

		public bool DoesRegionExist(int id)
		{
			return _db.Regions.Any(k => k.Id == id);
		}

		public bool DoesRegionExist(string name)
		{
			return _db.Regions.Any(k => k.Name.ToLower().Trim() == name.ToLower().Trim());
		}

		public ICollection<Region> GetAllRegions()
		{
			return _db.Regions.OrderBy(k => k.Name).ToList();
		}

		public Region GetRegion(int id)
		{
			return _db.Regions.FirstOrDefault(k => k.Id == id);
		}

		public bool TryCreateRegion(Region region)
		{
			_db.Regions.Add(region);
			return MartialArtsGymFinderFunctions.Save(_db);
		}

		public bool TryDeleteRegion(Region region)
		{
			_db.Regions.Remove(region);
			return MartialArtsGymFinderFunctions.Save(_db);
		}

		public bool TryUpdateRegion(Region region)
		{
			_db.Regions.Update(region);
			return MartialArtsGymFinderFunctions.Save(_db);
		}*/
	}
}
