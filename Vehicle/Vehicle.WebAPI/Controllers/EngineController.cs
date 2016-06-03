using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vehicle;

namespace Vehicle.WebAPI.Controllers
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
	    public void Start()
	    {
		    mEngine.Start();
	    } 
    }
}
