using FirstDemo.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstDemo.OA.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }

        /// <summary>
        /// 执行控制器中的方法前先执行此方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //if (Session["userInfo"] == null)
            bool isSucess = false;
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionid = Request.Cookies["sessionId"].Value;
                //根据该值查memcache
                object obj = Common.MemcacheHelper.Get(sessionid);
                if (obj != null)
                {
                    UserInfo userInfo = Common.SerializeHelper.DeserializeToObject<UserInfo>(obj.ToString());
                    LoginUser = userInfo;
                    isSucess = true;
                    Common.MemcacheHelper.Set(sessionid, obj, DateTime.Now.AddMinutes(20));//模拟出滑动过期时间
                }
                //filterContext.Result = Redirect("/Login/Index");
            }
            if (!isSucess)
            {
                filterContext.Result = Redirect("/Login/Index");
            }
        }
    }
}