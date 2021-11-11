using System.Web;

namespace Jikandesu.Areas.Authentication.Models
{
    public interface IUserProvider
    {
        User GetUser(HttpContextBase context);
    }
}