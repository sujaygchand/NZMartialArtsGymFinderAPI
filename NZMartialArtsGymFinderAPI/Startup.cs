using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NZMartialArtsGymFinderAPI.Data;
using NZMartialArtsGymFinderAPI.Mappers;
using NZMartialArtsGymFinderAPI.Repositories;
using NZMartialArtsGymFinderAPI.Repositories.IRepositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
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

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IRegionRepository , RegionRepository>();
			/*services.AddScoped<IMartialArtsRepository, MartialArtsRepository>();
			services.AddScoped<IGymRepository, GymRepository>();*/

			services.AddAutoMapper(typeof(MartialArtsGymFinderMappings));

			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			services.AddSwaggerGen();
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
			app.UseSwaggerUI(options => {
				options.SwaggerEndpoint("/swagger/NZMAGFOpenAPISpec/swagger.json", "NZ Martial Arts Gym Finder API");
				options.RoutePrefix = "";
			});
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
