using System.Web.Mvc;
using Jikandesu.Models;

namespace Jikandesu.Areas.Home
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Areas/Home/Views/Index.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
