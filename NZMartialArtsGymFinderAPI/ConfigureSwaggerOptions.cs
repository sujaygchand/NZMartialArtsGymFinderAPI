using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI
{
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		public void Configure(SwaggerGenOptions options)
		{
			options.SwaggerDoc("NZMAGFOpenAPISpec", new OpenApiInfo()
			{
				Title = "NZ Martial Arts Gym Finder API",
				Version = "1"
			});

			var xmlCommentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFileName);
			options.IncludeXmlComments(xmlCommentsFullPath);
		}
	}
}
