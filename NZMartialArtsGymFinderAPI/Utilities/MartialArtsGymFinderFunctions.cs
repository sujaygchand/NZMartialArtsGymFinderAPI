using NZMartialArtsGymFinderAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Utilities
{
	public static class MartialArtsGymFinderFunctions
	{
		public static bool Save(ApplicationDbContext db)
		{
			return db?.SaveChanges() >= 0;
		}
	}
}
