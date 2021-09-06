using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Mappers;
using NZMartialArtsGymFinderAPI.Repositories;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using ParkyAPI;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private const string APPLICATION_SETTINGS_SECTION = "ApplicationSettings";

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IRegionRepository, RegionRepository>();
			services.AddScoped<IMartialArtsRepository, MartialArtsRepository>();
			services.AddScoped<IGymRepository, GymRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddAutoMapper(typeof(MartialArtsGymFinderMappings));

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen();

			var appSettingsSection = Configuration.GetSection(APPLICATION_SETTINGS_SECTION);
			services.Configure<ApplicationSettings>(appSettingsSection);
			var appSettings = appSettingsSection.Get<ApplicationSettings>();
			services.AddScoped<ApplicationSettings>(k => appSettings);

			var appKey = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(k =>
			{
				k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(k =>
			{
				k.RequireHttpsMetadata = false;
				k.SaveToken = true;
				k.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(appKey),
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseSwagger();

			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/NZMAGFOpenAPISpec/swagger.json", "NZ Martial Arts Gym Finder API");
				options.RoutePrefix = "";
			});

			app.UseRouting();

			// Allows methods from different API versions to be used
			app.UseCors(k => k.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			// Always Authenticate before Authorize
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
