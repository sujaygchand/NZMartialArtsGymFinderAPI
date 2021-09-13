using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace NZMartialArtsGymFinderAPI
{
	public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
	{
		protected override void Init(IWebHostBuilder builder)
		{
			builder.UseStartup<Startup>();
		}

		public override Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandlerAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext lambdaContext)
		{
			return base.FunctionHandlerAsync(request, lambdaContext);
		}
	}
}
