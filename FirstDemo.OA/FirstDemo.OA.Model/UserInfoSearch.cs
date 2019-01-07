using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.OA.Model
{
    /// <summary>
    /// 构建搜索用户信息条件
    /// </summary>
    public class UserInfoSearch : BaseSeach
    {
        public string UserName { get; set; }
        public string UserRemark { get; set; }
    }
}
