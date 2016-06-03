using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Vehicle.WebAPISelfHost
{
	public class EngineController : ApiController
	{
		Engine mEngine;

		public EngineController()
		{
			mEngine = new Engine();
		}

		[HttpPost]
		[ActionName("StartEngine")]
		public string Start()
		{
			mEngine.Start();
			return "Started";
		}

		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

	}
}
