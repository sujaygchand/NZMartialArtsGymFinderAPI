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
	}
}
