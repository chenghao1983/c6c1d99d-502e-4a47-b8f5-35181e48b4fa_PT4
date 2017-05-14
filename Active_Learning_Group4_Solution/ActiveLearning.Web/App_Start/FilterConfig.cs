using System.Web;
using System.Web.Mvc;
using ActiveLearning.Web.Filter;

namespace ActiveLearning.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
