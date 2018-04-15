using System.Web.Mvc;

namespace FeedbackCba
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute { View = "Error" });
        }
    }
}
