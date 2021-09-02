using Microsoft.EntityFrameworkCore;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories.Generic
{
	public interface IGenericModelRepository<ModelType> where ModelType : BaseModel
	{
		ICollection<ModelType> GetAll(DbSet<ModelType> databaseSet);
		ModelType Get(int id, DbSet<ModelType> databaseSet);
		bool DoesEntryExist(int id, DbSet<ModelType> databaseSet);
		bool DoesEntryExist(string name, DbSet<ModelType> databaseSet);
		bool TryCreateEntry(ModelType model, ApplicationDbContext db, DbSet<ModelType> databaseSet);
		bool TryUpdateEntry(ModelType model, ApplicationDbContext db, DbSet<ModelType> databaseSet);
		bool TryDeleteEntry(ModelType model, ApplicationDbContext db, DbSet<ModelType> databaseSet);
	}
}
