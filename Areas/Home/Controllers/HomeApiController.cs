using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Jikandesu.Areas.Authentication.Models;
using Jikandesu.Models;

namespace Jikandesu.Areas.Home.Controllers
{
    public class HomeApiController : BaseApiController
    {
        private readonly IUserProvider _userProvider;

        public HomeApiController(
            IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        public void TestUser()
        {
            var user = _userProvider.GetUser(HttpContext);
            Trace.TraceInformation(user.UserId + "");
            Trace.TraceInformation(user.UserName);
            Trace.TraceInformation(user.Email);
            Response.Redirect("/");
        }
    }
}
