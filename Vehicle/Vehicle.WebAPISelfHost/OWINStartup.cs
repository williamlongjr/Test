using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Vehicle.WebAPISelfHost.OWINStartup))]

namespace Vehicle.WebAPISelfHost
{
	public class OWINStartup
	{
		public void Configuration(IAppBuilder app)
		{

			HttpConfiguration config = new HttpConfiguration();

			//config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			//config.Routes.MapHttpRoute(
			//	name: "CommandApi",
			//	routeTemplate: "api/{action}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			var hubConfiguration = new HubConfiguration();
			hubConfiguration.EnableDetailedErrors = true;

			app.MapSignalR(hubConfiguration);
//			app.UseWebApi(config);
			//app.MapConnection<RawConnection>("/raw", new ConnectionConfiguration { EnableCrossDomain = true });

		}
	}
}
