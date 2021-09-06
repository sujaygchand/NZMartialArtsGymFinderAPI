using NZMartialArtsGymFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		User Authenticate(string username, string password);
		User Register(string username, string password, string role = "");
	}
}
