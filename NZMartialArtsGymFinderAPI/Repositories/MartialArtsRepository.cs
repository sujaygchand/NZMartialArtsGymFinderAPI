using NZMartialArtsGymFinderAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Utilities;

namespace NZMartialArtsGymFinderAPI.Repositories
{
	public class MartialArtsRepository : IMartialArtsRepository
	{
		private readonly ApplicationDbContext _db;

		public MartialArtsRepository(ApplicationDbContext db)
		{
			_db = db ?? throw new Exception("ApplicationDbContext does not exist");
		}

		public bool DoesMartialArtExist(int id)
		{
			return _db.MartialArts.Any(k => k.Id == id);
		}

		public bool DoesMartialArtExist(string name)
		{
			return _db.MartialArts.Any(k => k.Name.ToLower().Trim() == name.ToLower().Trim());
		}

		public ICollection<MartialArt> GetAllMartialArts()
		{
			return _db.MartialArts.OrderBy(k => k.Name).ToList();
		}

		public MartialArt GetMartialArt(int id)
		{
			return _db.MartialArts.FirstOrDefault(k => k.Id == id);
		}

		public bool TryCreateMartialArt(MartialArt martialArt)
		{
			_db.MartialArts.Add(martialArt);
			return MartialArtsGymFinderFunctions.Save(_db);
		}

		public bool TryDeleteMartialArt(MartialArt martialArt)
		{
			_db.MartialArts.Remove(martialArt);
			return MartialArtsGymFinderFunctions.Save(_db);
		}

		public bool TryUpdateMartialArt(MartialArt martialArt)
		{
			_db.MartialArts.Update(martialArt);
			return MartialArtsGymFinderFunctions.Save(_db);
		}
	}
}
