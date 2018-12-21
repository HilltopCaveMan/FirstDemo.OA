using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace FirstDemo.OA.DALFactory
{
    public class DBSessionFactory
    {
        public static IDAL.IDBSession CreateDBSession()
        {
            IDAL.IDBSession dBSession = (IDAL.IDBSession)CallContext.GetData("dbSession");
            if (dBSession == null)
            {
                dBSession = new DBSession();
                CallContext.SetData("dbSession", dBSession);
            }
            return dBSession;
        }
    }
}