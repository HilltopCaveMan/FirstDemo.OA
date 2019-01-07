using FirstDemo.OA.Model;
using FirstDemo.OA.Model.EnumType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstDemo.OA.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
        public ActionResult Index()
        {

            return View();
        }

        #region 获取用户信息列表

        public ActionResult GetUserInfoList()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            //接收搜索条件
            string userName = Request["name"];
            string userRemark = Request["remark"];
            int totalCount = 0;
            //构建搜索条件
            UserInfoSearch userInfoSearch = new UserInfoSearch
            {
                UserName = userName,
                UserRemark = userRemark,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
            short delFlag = (short)DeleteEnumType.Normarl;
            var userInfoList = UserInfoService.LoadSeachEntities(userInfoSearch, delFlag);
            var temp = from u in userInfoList
                       select new
                       {
                           u.ID,
                           u.UName,
                           u.UPwd,
                           u.Remark,
                           u.SubTime
                       };
            return Json(new { rows = temp, total = userInfoSearch.TotalCount }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region 删除用户信息

        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #region 添加用户数据
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.DelFlag = 0;
            userInfo.ModifienOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }

        #endregion
        #region 展示要修改的数据
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = UserInfoService.LoadEntities(c => c.ID == id).FirstOrDefault();
            return Json(userInfo, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region 完成用户数据的更新
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            userInfo.ModifienOn = DateTime.Now;
            if (UserInfoService.EditEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
    }

}