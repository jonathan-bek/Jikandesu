using System.Web.Mvc;

namespace Jikandesu.Areas.Home
{
    public class MangaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Manga";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Manga_default",
                "Manga/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}