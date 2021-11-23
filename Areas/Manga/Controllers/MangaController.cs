using System.Web.Mvc;
using Jikandesu.Models;

namespace Jikandesu.Areas.Manga
{
    public class MangaController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Areas/Manga/Views/Index.cshtml");
        }

        public ActionResult Page(string searchUrl)
        {
            return View("~/Areas/Manga/Views/MangaPage.cshtml");
        }
    }
}