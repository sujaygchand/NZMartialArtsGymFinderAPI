using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using NZMartialArtsGymFinderAPI.Utilities;
using ParkyAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly ApplicationSettings _appSettings;

		private DateTime bearerExpiration = DateTime.UtcNow.AddDays(7);

		public UserRepository(ApplicationDbContext db, ApplicationSettings appSettings)
		{
			_db = db ?? throw new Exception("ApplicationDbContext is null");
			_appSettings = appSettings ?? throw new Exception("ApplicationSettings is null");
		}

		public User Authenticate(string username, string password)
		{
			var user = _db.Users.SingleOrDefault(k => k.Username == username );

			// User not found
			if (user == null)
				return null;

			if (!DoesPasswordMatch(password, user.Password, user.PasswordKey))
				return null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[] {
				new Claim(ClaimTypes.Name, user.Role.ToString()),
				new Claim(ClaimTypes.Role, user.Role.ToString())
				}),
				Expires = bearerExpiration,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			user.Token = tokenHandler.WriteToken(token);
			return user;
		}

		public bool DoesPasswordMatch(string passwordText, byte[] passwordBytes, byte[] passwordKey)
		{
			using(var hmac = new HMACSHA512(passwordKey))
			{
				var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));

				int passwordHashLength = passwordHash.Length;

				if (passwordHashLength != passwordBytes.Length)
					return false;

				for (int i = 0; i < passwordHashLength; i++)
				{
					if (passwordBytes[i] != passwordHash[i])
						return false;
				}
			}


			return true;
		}

		public ICollection<User> GetAllUsers()
		{
			return _db.Users.OrderBy(k => k.Id).ToList();
		}

		public User GetUser(int id)
		{
			if (_db.Users == null)
				return null;

			return _db.Users.FirstOrDefault(k => k.Id == id);
		}

		public bool IsUniqueUser(string username)
		{
			var user = _db.Users.SingleOrDefault(k => k.Username == username);

			return user == null;
		}

		public User Register(AuthenticationModel authenticationModel)
		{
			byte[] passwordHash, passwordKey;

			using(var hmac = new HMACSHA512())
			{
				passwordKey = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(authenticationModel.Password));
			}

			User user = new User()
			{
				Username = authenticationModel.Username,
				Password = passwordHash,
				PasswordKey = passwordKey,
				Role = string.IsNullOrWhiteSpace(authenticationModel.Role) ? "standard" : authenticationModel.Role,
			};

			_db.Users.Add(user);
			_db.SaveChanges();
			user.Password = null;   // Hides the password from being shown on response
			return user;
		}

		public bool TryDeleteUser(User user)
		{
			_db.Users.Remove(user);
			return MartialArtsGymFinderFunctions.Save(_db);
		}
	}
}
