using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaiMingAI.DataHelper;
using TaiMingAI.DBTools;
using TaiMingAI.Manager.Model;
using Dapper;

namespace TaiMingAI.Manager.DAL
{
    public class NavbarDal : BaseDal<Navbar>
    {
        #region 属性
        private static NavbarDal dal;
        private NavbarDal() : base(ManagerConfig.DbManager) { }
        public static NavbarDal CreatDal()
        {
            if (dal == null)
            {
                lock (obj)
                {
                    if (dal == null)
                    {
                        dal = new NavbarDal();
                    }
                }
            }
            return dal;
        }
        #endregion
    }
}
