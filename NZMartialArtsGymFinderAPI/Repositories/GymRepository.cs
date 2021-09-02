using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.Generic;

namespace NZMartialArtsGymFinderAPI.Repositories
{
	public class GymRepository : GenericModelRepository<Gym>, IGymRepository
	{
	}
}
