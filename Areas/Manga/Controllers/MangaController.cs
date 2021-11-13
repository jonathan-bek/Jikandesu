using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jikandesu.Areas.Manga
{
    public class MangaController : Controller
    {
        // GET: Manga/Manga
        public ActionResult Index()
        {
            return View();
        }
    }
}