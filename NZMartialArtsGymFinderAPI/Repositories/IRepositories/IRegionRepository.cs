using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IRegionRepository : IGenericModelRepository<Region>
	{
		/*ICollection<Region> GetAllRegions();
		Region GetRegion(int id);
		bool DoesRegionExist(int id);
		bool DoesRegionExist(string name);
		bool TryCreateRegion(Region region);
		bool TryUpdateRegion(Region region);
		bool TryDeleteRegion(Region region);*/
	}
}
