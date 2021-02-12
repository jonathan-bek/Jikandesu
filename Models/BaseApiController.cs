using System.Diagnostics;
using System.Web.Mvc;

namespace Jikandesu.Models
{
    public class BaseApiController : BaseController
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            Trace.TraceError(filterContext.Exception.Message);
            filterContext.ExceptionHandled = true;
            filterContext.Result = ExceptionJsonContent(filterContext.Exception);
        }
    }
}
