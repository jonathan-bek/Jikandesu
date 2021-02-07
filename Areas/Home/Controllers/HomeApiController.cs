using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : Controller
    {
        // GET: Home/HomeApi
        public ContentResult GetInt()
        {
            return new ContentResult() { Content = "1" };
        }
    }
}