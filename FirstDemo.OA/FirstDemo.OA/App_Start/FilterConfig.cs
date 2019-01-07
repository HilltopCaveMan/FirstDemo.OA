using FirstDemo.OA.Models;
using System.Web;
using System.Web.Mvc;

namespace FirstDemo.OA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());
        }
    }
}
