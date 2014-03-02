using System;
using System.Web.Mvc;
using MvcDI;
using TestApplication.WorkerServices;
using TestApplication.WorkerServices.Implement;

namespace TestApplication.Controllers
{
    public class TestController : Controller
    {
        [Implement(typeof(TestService), true,DebugImplementType=typeof(TestService2))]
        private ITestService service = null;
        //
        // GET: /Test/

        public ActionResult Index()
        {
            if (service is TestService)
            {
                return View();
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}
