using NZMartialArtsGymFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IUserRepository
	{
		public ICollection<User> GetAllUsers();
		public User GetUser(int id);
		public bool TryDeleteUser(User user);
		bool IsUniqueUser(string username);
		User Authenticate(string username, string password);
		User Register(AuthenticationModel authenticationModel);
	}
}
