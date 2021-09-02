using Microsoft.EntityFrameworkCore;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories.Generic;
using NZMartialArtsGymFinderAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.Generic
{
	public class GenericModelRepository<T> : IGenericModelRepository<T> where T: BaseModel
	{
		public bool DoesEntryExist(int id, DbSet<T> databaseSet)
		{
			return databaseSet.Any(k => k.Id == id);
		}

		public bool DoesEntryExist(string name, DbSet<T> databaseSet)
		{
			return databaseSet.Any(k => k.Name.ToLower().Trim() == name.ToLower().Trim());
		}

		public T Get(int id, DbSet<T> databaseSet)
		{
			return databaseSet.FirstOrDefault(k => k.Id == id);
		}

		public ICollection<T> GetAll(DbSet<T> databaseSet)
		{
			return databaseSet.OrderBy(k => k.Name).ToList();
		}

		public bool TryCreateEntry(T model, ApplicationDbContext db, DbSet<T> databaseSet)
		{
			databaseSet.Add(model);
			return MartialArtsGymFinderFunctions.Save(db);
		}

		public bool TryDeleteEntry(T model, ApplicationDbContext db, DbSet<T> databaseSet)
		{
			databaseSet.Remove(model);
			return MartialArtsGymFinderFunctions.Save(db);
		}

		public bool TryUpdateEntry(T model, ApplicationDbContext db, DbSet<T> databaseSet)
		{
			databaseSet.Update(model);
			return MartialArtsGymFinderFunctions.Save(db);
		}
	}
}
