using System.Web.Mvc;
 
namespace DHX.Scheduler.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Basic
        public ActionResult Basic()
        {
            return View();
        }

        // GET: Recurring
        public ActionResult Recurring()
        {
            return View();
        }
    }
}