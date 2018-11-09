using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;
using TaiMingAI.Manager.Model;

namespace TaiMingAI.Manager.DAL
{
    public class RoleDal : BaseDal<Role>
    {
        #region 属性
        private static RoleDal dal;
        private RoleDal() : base(ManagerConfig.DbManager) { }
        public static RoleDal CreatDal()
        {
            if (dal == null)
            {
                lock (obj)
                {
                    if (dal == null)
                    {
                        dal = new RoleDal();
                    }
                }
            }
            return dal;
        }
        #endregion
    }
}
