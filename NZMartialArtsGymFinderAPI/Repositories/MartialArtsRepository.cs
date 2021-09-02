using NZMartialArtsGymFinderAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Utilities;
using NZMartialArtsGymFinderAPI.Repositories.Generic;

namespace NZMartialArtsGymFinderAPI.Repositories
{
	public class MartialArtsRepository : GenericModelRepository<MartialArt>, IMartialArtsRepository
	{
	}
}
