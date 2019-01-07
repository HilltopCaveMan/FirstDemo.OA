using FirstDemo.OA.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FirstDemo.OA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//读取了配置文件中关于Log4net配置信息
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //开启一个线程，扫描异常信息队列
            string filePath = Server.MapPath("/log/");
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    //判断一下队列中是否有数据
                    if (MyExceptionAttribute.ExceptionQueue.Count() > 0)
                    {
                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();
                        if (ex != null)
                        {
                            //将异常信息写到日志文件中
                            ILog log = LogManager.GetLogger("errorMsg");
                            log.Error(ex.ToString());
                        }
                        else
                        {
                            //如果队列中没有数据，休息
                            Thread.Sleep(3000);
                        }


                    }
                    else
                    {
                        //如果队列中没有数据，休息
                        Thread.Sleep(3000);
                    }
                }
            }, filePath);
        }
    }
}
