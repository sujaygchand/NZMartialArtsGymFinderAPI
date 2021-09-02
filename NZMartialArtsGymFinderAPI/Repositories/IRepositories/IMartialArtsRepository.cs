using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IMartialArtsRepository : IGenericModelRepository<MartialArt>
	{
		/*ICollection<MartialArt> GetAllMartialArts();
		MartialArt GetMartialArt(int id);
		bool DoesMartialArtExist(int id);
		bool DoesMartialArtExist(string name);
		bool TryCreateMartialArt(MartialArt martialArt);
		bool TryUpdateMartialArt(MartialArt martialArt);
		bool TryDeleteMartialArt(MartialArt martialArt);*/
	}
}
