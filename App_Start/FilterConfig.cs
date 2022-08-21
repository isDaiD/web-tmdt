using System.Web;
using System.Web.Mvc;

namespace LeDaiDuong_151901766
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
