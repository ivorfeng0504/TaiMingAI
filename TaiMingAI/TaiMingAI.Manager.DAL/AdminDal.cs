using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;
using TaiMingAI.Manager.Model;
using Dapper;

namespace TaiMingAI.Manager.DAL
{
    public class AdminDal : BaseDal<Administrator>
    {
        #region 属性
        private static AdminDal adminDal;
        private AdminDal() : base(ManagerConfig.DbManager) { }
        public static AdminDal CreatDal()
        {
            if (adminDal == null)
            {
                lock (obj)
                {
                    if (adminDal == null)
                    {
                        adminDal = new AdminDal();
                    }
                }
            }
            return adminDal;
        }
        #endregion
    }
}
