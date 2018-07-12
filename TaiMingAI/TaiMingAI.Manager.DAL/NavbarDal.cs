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

namespace TaiMingAI.Manager.DAL
{
    public class NavbarDal
    {
        private DBContext dBContext = DBContext.InitDBContext;
        public List<NavBardb> GetNavbarList()
        {
            var sql = dBContext.GetSql("NavbarSql.GetNavbarList");
            var result = dBContext.ExecuteDataTable(ManagerConfig.DbManager, CommandType.Text, sql, null);
            return DataConvertHelper.DataTableToList<NavBardb>(result);
        }

        public bool InsertNavber(NavBardb navBar)
        {
            var sql = dBContext.GetSql("NavbarSql.InsertNavber");
            SqlParameter[] sqlPar = new SqlParameter[] {
                new SqlParameter("@ParentId",navBar.ParentId),
                new SqlParameter("@title",navBar.title),
                new SqlParameter("@icon",navBar.icon),
                new SqlParameter("@href",navBar.href),
                new SqlParameter("@spread",navBar.spread),
                new SqlParameter("@target",navBar.target),
                new SqlParameter("@IsShow",navBar.IsShow),
                new SqlParameter("@Sort",navBar.Sort)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlPar);
            return result > 0;
        }

        public bool UpdateNavber(NavBardb navBar)
        {
            var sql = dBContext.GetSql("NavbarSql.UpdateNavber");
            SqlParameter[] sqlPar = new SqlParameter[] {
                new SqlParameter("@ParentId",navBar.ParentId),
                new SqlParameter("@title",navBar.title),
                new SqlParameter("@icon",navBar.icon),
                new SqlParameter("@href",navBar.href),
                new SqlParameter("@spread",navBar.spread),
                new SqlParameter("@target",navBar.target),
                new SqlParameter("@IsShow",navBar.IsShow),
                new SqlParameter("@Sort",navBar.Sort),
                new SqlParameter("@Id",navBar.Id)
            };
            var result = dBContext.ExecuteNonQuery(ManagerConfig.DbManager, CommandType.Text, sql, sqlPar);
            return result > 0;
        }
    }
}
