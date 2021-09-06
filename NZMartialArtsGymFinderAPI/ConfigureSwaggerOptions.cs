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
		private SwaggerGenOptions swaggerGenOptions;
		public void Configure(SwaggerGenOptions options)
		{
			swaggerGenOptions = options ?? throw new Exception("SwaggerGenOptions is null");

			swaggerGenOptions.SwaggerDoc("NZMAGFOpenAPISpec", new OpenApiInfo()
			{
				Title = "NZ Martial Arts Gym Finder API",
				Version = "1"
			});


			InitialiseXmlDocumentation();
			InitialiseBearerSecurityRequirement();
		}

		private void InitialiseBearerSecurityRequirement()
		{
			const string Bearer = "Bearer";

			swaggerGenOptions.AddSecurityDefinition(Bearer, new OpenApiSecurityScheme
			{
				Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
			   "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
			   "Example: \"Bearer 12345abcdef\"",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = Bearer
			});

			swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = Bearer
						},
						Scheme = "oauth2",
						Name = Bearer,
						In = ParameterLocation.Header
					},
					new List<string>()
				}
			});

		}

		private void InitialiseXmlDocumentation()
		{
			var xmlCommentFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFileName);
			swaggerGenOptions.IncludeXmlComments(xmlCommentsFullPath);
		}
	}
}
