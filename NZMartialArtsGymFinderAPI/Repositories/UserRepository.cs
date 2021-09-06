﻿using Microsoft.IdentityModel.Tokens;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Models;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using ParkyAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
			var user = _db.Users.SingleOrDefault(k => k.Username == username && k.Password == password);

			// User not found
			if (user == null)
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
			user.Password = string.Empty;
			return user;
		}

		public bool IsUniqueUser(string username)
		{
			var user = _db.Users.SingleOrDefault(k => k.Username == username);

			return user == null;
		}

		public User Register(string username, string password, string role = "")
		{
			User user = new User()
			{
				Username = username,
				Password = password,
				Role = string.IsNullOrWhiteSpace(role) ? "Standard" : role,
			};

			_db.Users.Add(user);
			_db.SaveChanges();
			user.Password = string.Empty;   // Hides the password from being shown on response
			return user;
		}
	}
}