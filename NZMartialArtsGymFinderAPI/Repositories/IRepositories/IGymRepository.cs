using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IGymRepository : IGenericModelRepository<Gym>
	{
		/*ICollection<Gym> GetAllGyms();
		Gym GetGym(int id);
		bool DoesGymExist(int id);
		bool DoesGymExist(string name);
		bool TryCreateGym(Gym gym);
		bool TryUpdateGym(Gym gym);
		bool TryDeleteGym(Gym gym);*/
	}
}
