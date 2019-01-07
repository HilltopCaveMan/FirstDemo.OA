using FirstDemo.OA.IBLL;
using FirstDemo.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FirstDemo.OA.BLL
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        /// <summary>
        /// 批量删除多条用户数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.CurrentDBSession.UserInfoDal.LoadEntities(c => list.Contains(c.ID));
            foreach (var userInfo in userInfoList)
            {
                this.CurrentDBSession.UserInfoDal.DeleteEntity(userInfo);
            }
            return this.CurrentDBSession.SaveChange();
        }

        public IQueryable<UserInfo> LoadSeachEntities(UserInfoSearch userInfoSearch, short dleFlag)
        {
            var temp = this.CurrentDBSession.UserInfoDal.LoadEntities(c => c.DelFlag == dleFlag);
            if (!string.IsNullOrEmpty(userInfoSearch.UserName))
            {
                temp = temp.Where<UserInfo>(c => c.UName.Contains(userInfoSearch.UserName));
            }
            if (!string.IsNullOrEmpty(userInfoSearch.UserRemark))
            {
                temp = temp.Where<UserInfo>(u => u.Remark.Contains(userInfoSearch.UserRemark));
            }
            userInfoSearch.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, int>(u => u.ID).Skip<UserInfo>((userInfoSearch.PageIndex - 1) * userInfoSearch.PageSize).Take<UserInfo>(userInfoSearch.PageSize);
        }

        public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserInfoDal;
        }
    }
}