using NZMartialArtsGymFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories.IRepositories
{
	public interface IUserRepository
	{
		ICollection<User> GetAllUsers();
		User GetUser(int id);
		bool TryDeleteUser(User user);
		bool IsUniqueUser(string username);
		User Authenticate(string username, string password);
		bool DoesPasswordMatch(string passwordText, byte[] passwordBytes, byte[] passwordKey);
		User Register(AuthenticationModel authenticationModel);
	}
}
