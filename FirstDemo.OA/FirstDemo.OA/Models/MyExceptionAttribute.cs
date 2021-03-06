﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstDemo.OA.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        public static Queue<Exception> ExceptionQueue = new Queue<Exception>();
        /// <summary>
        /// 可以捕获异常数据
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Exception ex = filterContext.Exception;
            //写入队列
            ExceptionQueue.Enqueue(ex);
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}