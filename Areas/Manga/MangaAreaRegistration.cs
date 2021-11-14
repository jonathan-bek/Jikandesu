using System.Web.Mvc;

namespace Jikandesu.Areas.Manga
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
                "Manga/{controller}/{action}",
                new { action = "Index" }
            );
        }
    }
}