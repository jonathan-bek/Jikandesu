using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Areas.Home.Models.MangaData;
using Jikandesu.Models;

namespace Jikandesu.Areas.Manga
{
    public class MangaController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Areas/Manga/Views/Index.cshtml");
        }
    }
}