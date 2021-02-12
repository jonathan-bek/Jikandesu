using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Jikandesu.Models
{
    public class BaseController : Controller
    {
        public BaseController() { }

        protected ContentResult SuccessJsonContent(object data)
        {
            return JsonContent(data);
        }

        protected ContentResult ExceptionJsonContent(Exception e)
        {
            var msgs = new string[] { e.Message };
            return JsonContent(msgs);
        }

        private ContentResult JsonContent(object data)
        {
            var str = JsonConvert.SerializeObject(data);
            return Content(str, "application/json");
        }
    }
}
